export default {
    password: {
        Title: "Password Settings",
        Validity: "密码有效性",
        MinLength: "最小密码长度",
        IncludeNumber: "包含数字",
        IncludeLower: "包含小写字母",
        IncludeUpper: "包含大写字母",
        IncludeSpecial: "包含特殊字符",
        Security: "密码安全",
        ChangePassUponFirstLogin: "首次登录需修改密码",
        CanNotRepeatedTimes: "近几次密码不可重复",
        ExpiryPeriod: "密码几天后过期，超期后需修改密码",
        InvalidLoginAttempts: "无效登录尝试几次后，锁定该账户",
        ZeroForNotSet: "（0表示不限定）"
    },
    email: {
        Title: "Email",
        Server: "服务器地址",
        Port: "端口",
        Address: "发送者地址",
        Password: "密码",
        EnableSSL: "是否启用SSL",
        validator: {
            Server: "服务器地址不正确",
            AddressWrongFormat: "邮件发送地址不正确!",
            PortRange: "端口在1~65535之间"
        }
    },
    importExport: {
        Title: "importExport",
        Location: "Location",
        Site: "Site",
        Equipment: "Equipment",
        Media: "Media",
        Organism: "Organism",
        button: {
            Import: "Import",
            Export: "Export",
            Download: "Download",
            DownloadError: "DownloadError"
        },
        lable: {
            Empty: "Not uploaded",
            Tips: "Warm Reminder",
            Description: "The imported file must be a template exported from this system. If not, please download the template first and fill in the corresponding data according to the specifications",
            TipsLable: "Import Instructions",
            Tip1: "1. The content marked with a red * in the template is mandatory",
            Tips2: "2. Please read the prompts in the template carefully and fill in according to the specifications. Incorrect formats cannot be imported",
            Tips3: ""
        }
    },
    lable: {
        Error: "Set Error",
        Success: "Set Success"
    }
}