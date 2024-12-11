export default {
    password: {
        Title: "密码",
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
        Title: "邮件通知",
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
    importExport:{
        Title: "主数据导入导出",
        Location:"区域",
        Site:"采样点",
        Equipment:"仪器设备",
        Media:"试剂耗材",
        Organism:"菌种",
        button:{
            Import:"导入",
            Export:"导出",
            Download:"下载模板",
            DownloadError:"下载错误信息"
        },
        lable:{
            Empty:"未上传",
            Tips:"温馨提示",
            Description:"导入的文件必须为本系统导出的模板，如果不是请先下载模板 ，并按照规范填写相应的数据",
            TipsLable:"导入说明",
            Tip1:"1. 模板中带红色*号的为必填内容",
            Tips2:"2. 请详细阅读模板中的提示内容并按照规范填写，格式不正确的无法导入",
            Tips3:""
        }
    },
    lable: {
        Error: "设置失败",
        Success: "设置成功"
    }
}