# 创建自签名证书
$cert = New-SelfSignedCertificate -DnsName "yourdomain.com" -CertStoreLocation "cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(1)

# 确保导出证书的目录存在
$certPath = "D:\项目资料\05EClient\EClient\src\EHost.Mqtt\cert"
if (-not (Test-Path -Path $certPath)) {
    New-Item -ItemType Directory -Path $certPath
}

# 将证书导出到文件（确保包含文件扩展名 .cer）
Export-Certificate -Cert $cert -FilePath "$certPath\cert.cer"

# 如果需要PFX文件，使用以下命令（确保包含文件扩展名 .pfx）
$pfxPath = "D:\项目资料\05EClient\EClient\src\EHost.Mqtt\test.pfx"
$securePassword = ConvertTo-SecureString -String "YourPassword" -Force -AsPlainText
Export-PfxCertificate -Cert $cert -FilePath $pfxPath -Password $securePassword
