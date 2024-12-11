$SLN_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "src", "frontend")
$RELEASE_PATH = [IO.Path]::Combine("${PSScriptRoot}", "..", "src", "frontend", "dist")
Set-Location $SLN_PATH

npm install --force
powershell ./build_tenant.ps1 -TITLE EMIS -TENANT Emis # -PUBLIC_PATH Emis

Set-Location ${PSScriptRoot}
