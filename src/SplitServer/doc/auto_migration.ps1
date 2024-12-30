param
(
    [Parameter(Mandatory)] [string] $CTX,
    [Parameter(Mandatory)] [string] $DB_TYPE
)
function run_cmd($cmd) {
#    echo $cmd
    Invoke-Expression $cmd
}
function migrate($ctx) {
  $MIG_LIST = dotnet ef migrations list -c ${ctx}DbContext --no-build -p $DB_PATH
  Write-Host $MIG_LIST -ForegroundColor:Blue 
  if ($MIG_LIST.EndsWith('No migrations were found.') -eq $true)
  {
      $MIG_VERSION = 1;
  }
  else
  {
      $MIG_VERSION = [int]$MIG_LIST.Split(".")[-1].Split(" ")[0] + 1
  }
#  echo $MIG_VERSION
  run_cmd "dotnet ef migrations add v0.0.${MIG_VERSION} -s ${DB_PATH} -c ${ctx}DbContext --no-build --prefix-output --output-dir ${ctx}"
}
Clear

$MIGRATION_PROJ = "ESys.Db.${DB_TYPE}"
$MIGRATION_CFG = "Migration${DB_TYPE}"
$env:NETCORE_ENVIRONMENT=${MIGRATION_CFG}
$DB_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "backend", ${MIGRATION_PROJ})

$SLN_PATH = [IO.Path]::Combine("${PSScriptRoot}","..", "backend")
$IGNORE_PROJECT = "ESys.Client","ESys.Lasair","ESys.Device","EntityViewGenerator","Tools","ESys.Importer"
dotnet clean -v q --nologo $SLN_PATH
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
    if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("ESys.")))
    {
        if($IGNORE_PROJECT -notcontains $_.Name)
        {
            Write-Host "[Building $_ for Migration]" -ForegroundColor:Green
            dotnet build -v q --nologo -p:CheckEolTargetFramework=false -p:ExtraDefineConstants=MIGRATION $_.FullName
        }
    }
}

migrate($CTX)

dotnet clean -v q --nologo $SLN_PATH
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
    if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("ESys.")))
    {
        if($IGNORE_PROJECT -notcontains $_.Name)
        {
            Write-Host "[Building $_]" -ForegroundColor:Green
            dotnet build -v q --nologo -p:CheckEolTargetFramework=false $_.FullName
        }
    }
}
