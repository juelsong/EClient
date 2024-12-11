export default {
    entity: '用户管理',
    filter: {
        Department: "部门",
        RealName: "姓名",
        IsActive: "显示禁用",
    },
    column: {
        Account: "账号",
        RealName: "姓名",
        Password: "密码",
        EmployeeId: "员工编号",
        EMail: "电子邮件",
        Phone: "电话",
        Department: "部门",
        Title: "职位",
        Location: "区域",
        Roles: "角色",
    },
    editor: {
        Account: "账号",
        RealName: "姓名",
        EmployeeId: "员工编号",
        OriPassword: "原密码",
        Password: "密码",
        Password2: "确认密码",
        EMail: "电子邮件",
        Phone: "电话",
        Department: "部门",
        Title: "职位",
        Location: "区域",
        Roles: "角色",
    },
    validator: {
        PasswordNotSame: "两次输入密码不一致!",
    }
}
