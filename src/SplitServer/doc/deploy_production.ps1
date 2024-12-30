$SLN_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "backend")
$RELEASE_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "backend", "Release_Production")
$IGNORE_PROJECT = "ESys.Client","EntityViewGenerator","Tools","ESys.ODataGenerator","ESys.Importer"
$REPORT_LIB_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "lib", "report")
if([System.IO.Directory]::Exists($RELEASE_PATH))
{
    rm -r -force $RELEASE_PATH
}
Get-ChildItem $SLN_PATH | ForEach-Object -Process{
	if($_ -is [System.IO.DirectoryInfo] -and ($_.Name.StartsWith("ESys.")))
	{
		if($IGNORE_PROJECT -notcontains $_.Name)
		{
			Write-Host "[Building $_]" -ForegroundColor:Green
			dotnet publish -c Release -r win-x64 -o $RELEASE_PATH -v q --nologo --self-contained=false -p:ExtraDefineConstants=PRODUCTION $_.FullName
		}
	}
}
# copy "${REPORT_LIB_PATH}/*.dll" $RELEASE_PATH
