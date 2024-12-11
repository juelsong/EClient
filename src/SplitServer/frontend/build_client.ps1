$SCRIPT_PATH = Get-Location
cd ${PSScriptRoot}
$DIST_PATH = [IO.Path]::Combine("${PSScriptRoot}", "dist")
node ./node_modules/@vue/cli-service/bin/vue-cli-service.js build --mode client
$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
Get-ChildItem -Recurse $DIST_PATH | ForEach-Object -Process{
	if($_ -is [System.IO.FileInfo] -and ($_.Name.EndsWith(".js")))
	{
        Write-Host "[Process $_]" -ForegroundColor:Green
		$CONTENT = Get-Content $_.FullName -Encoding UTF8
		$CONTENT = $CONTENT -replace '"___API_BASE_URL___"',"window.getBaseUrl()"
		$CONTENT = $CONTENT -replace '"___APP_TENANT___"',"window.getTenant()"
		[System.IO.File]::WriteAllLines($_.FullName, $CONTENT, $Utf8NoBomEncoding)
	}
}

cd $SCRIPT_PATH