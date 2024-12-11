param
(
    [Parameter(Mandatory)] [string] $TITLE,
    [Parameter(Mandatory)] [string] $TENANT,
    [Parameter(Mandatory=$false)] [string] $PUBLIC_PATH = "",
    [Parameter(Mandatory=$false)] [string] $BASE = "https://emis.metaura.cn:9445"
)
# clear
$SCRIPT_PATH = Get-Location
cd ${PSScriptRoot}
$DIST_PATH = [IO.Path]::Combine("${PSScriptRoot}", "dist")
$TENANT_PATH = [IO.Path]::Combine("${PSScriptRoot}", "${TENANT}");
if( [IO.Directory]::Exists($TENANT_PATH) )
{
    [IO.Directory]::Delete($TENANT_PATH, $true)
}
node ./node_modules/@vue/cli-service/bin/vue-cli-service.js build --mode tenant
$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
Get-ChildItem -Recurse $DIST_PATH | ForEach-Object -Process{
	if($_ -is [System.IO.FileInfo] -and ($_.Name.EndsWith(".js") -or $_.Name.EndsWith(".html")))
	{
        Write-Host "[Process $_]" -ForegroundColor:Green
		$CONTENT = Get-Content $_.FullName -Encoding UTF8
		$CONTENT = $CONTENT -replace '/___APP_TENANT___/', "/${TENANT}/"
		$CONTENT = $CONTENT -replace '"___APP_TENANT___"', """${TENANT}"""
		$CONTENT = $CONTENT -replace '"___APP_TITLE___"', """${TITLE}"""
		$CONTENT = $CONTENT -replace '"___APP_BASE_URL___"', """${BASE}"""
		if(${MIGRATION}.length -eq 0)
		{
			$CONTENT = $CONTENT -replace '/___APP_PUBLIC_PATH___/', "/"
		}
		else
		{
			$CONTENT = $CONTENT -replace '/___APP_PUBLIC_PATH___/', "/${PUBLIC_PATH}/"
		}
		[System.IO.File]::WriteAllLines($_.FullName, $CONTENT, $Utf8NoBomEncoding)
	}
}

MOVE-Item $DIST_PATH -Destination $TENANT_PATH
cd $SCRIPT_PATH
