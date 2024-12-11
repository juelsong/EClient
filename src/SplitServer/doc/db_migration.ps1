param
(
    [Parameter(Mandatory=$false)] [string] $CTX = 'TenantSlave',
    [Parameter(Mandatory=$false)] [string] $MIGRATION = "",
    [Parameter(Mandatory=$true)] [string] $DB_TYPE
)

function migrate($ctx) {
  if(${MIGRATION}.length -eq 0)
  {
    $MIG_LIST = dotnet ef migrations list -c ${ctx}DbContext --no-build -p $DB_PATH
    $MIG_LIST = $MIG_LIST -replace "\(Pending\)",""
    $MIGRATION = $MIG_LIST.Split("\\r\\n", [System.StringSplitOptions]::RemoveEmptyEntries)[-1].Trim()
    Write-Host "[deduce Migration version $MIGRATION]" -ForegroundColor:Yellow
  }
  dotnet ef database update ${MIGRATION} -s ${DB_PATH} -c ${ctx}DbContext --no-build
}
Clear

$MIGRATION_PROJ = "ESys.Db.${DB_TYPE}"
$MIGRATION_CFG = "Migration${DB_TYPE}"
$env:NETCORE_ENVIRONMENT=${MIGRATION_CFG}
$DB_PATH = [IO.Path]::Combine("${PSScriptRoot}",  "backend", ${MIGRATION_PROJ})

$SLN_PATH = [IO.Path]::Combine("${PSScriptRoot}", "backend")
$IGNORE_PROJECT = "ESys.Client","EntityViewGenerator","Tools","ESys.Importer","ESys.Device","ESys.Lasair"
dotnet clean -v q --nologo $SLN_PATH
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
    if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("ESys.")) -and (-not $_.Name.Contains("UnitTest")))
    {
        if($IGNORE_PROJECT -notcontains $_.Name)
        {
            Write-Host "[Building $_ for Migration]" -ForegroundColor:Green
            dotnet build -v q --nologo -p:CheckEolTargetFramework=false -p:ExtraDefineConstants=MIGRATION $_.FullName
        }
        else
        {
            Write-Host "[Skip Building $_ for Migration]" -ForegroundColor:Yellow
        }
    }
}

migrate($CTX)

dotnet clean -v q --nologo $SLN_PATH
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
    if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("ESys.")) -and (-not $_.Name.Contains("UnitTest")))
    {
        if($IGNORE_PROJECT -notcontains $_.Name)
        {
            Write-Host "[Building $_]" -ForegroundColor:Green
            dotnet build -v q -p:CheckEolTargetFramework=false --nologo $_.FullName
        }
        else
        {
            Write-Host "[Skip Building $_]" -ForegroundColor:Yellow
        }
    }
}
