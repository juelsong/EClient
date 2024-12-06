# 定义证书指纹和导出路径
$thumbprint = "F0DA24B6516E96825B3FF3F5A8A617F6F15F9E4F"
$certPath = "D:\项目资料\05EClient\EClient\src\EHost.Mqtt\cert"
$cerFilePath = "$certPath\yourdomain.com.cer"
$pfxFilePath = "$certPath\yourdomain.com.pfx"
$securePassword = ConvertTo-SecureString -String "YourPassword" -Force -AsPlainText

# 导出 .cer 文件
Export-Certificate -Cert (Get-ChildItem -Path "cert:\LocalMachine\My\$thumbprint") -FilePath $cerFilePath

# 导出 .pfx 文件
Export-PfxCertificate -Cert (Get-ChildItem -Path "cert:\LocalMachine\My\$thumbprint") -FilePath $pfxFilePath -Password $securePassword
