$SLN_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "src", "backend")
$RELEASE_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "src", "backend", "Release_Production")
$IGNORE_PROJECT = "EMIS.Client","EntityViewGenerator","Tools","EMIS.ODataGenerator","EMIS.Importer"
$REPORT_LIB_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "lib", "report")
if([System.IO.Directory]::Exists($RELEASE_PATH))
{
    rm -r -force $RELEASE_PATH
}
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
	if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("EMIS.")))
	{
		if($IGNORE_PROJECT -notcontains $_.Name)
		{
			Write-Host "[Building $_]" -ForegroundColor:Green
			dotnet publish -c Release -r win-x64 -o $RELEASE_PATH -v q --nologo --self-contained=false -p:ExtraDefineConstants=PRODUCTION $_.FullName
		}
	}
}
# copy "${REPORT_LIB_PATH}/*.dll" $RELEASE_PATH
