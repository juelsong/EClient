export default {
    entity: 'User',
    filter: {
        Department: "Name",
        RealName: "RealName",
        IsActive: "IsActive",
    },
    column: {
        Account: "Account",
        RealName: "RealName",
        Password: "Password",
        EmployeeId: "EmployeeId",
        EMail: "EMail",
        Phone: "Phone",
        Department: "Name",
        Title: "Title",
        Location: "Name",
        Roles: "Name",
    },
    editor: {
        Account: "Account",
        RealName: "RealName",
        EmployeeId: "EmployeeId",
        OriPassword: "原密码",
        Password: "Password",
        Password2: "Confirm Password",
        EMail: "EMail",
        Phone: "Phone",
        Department: "Name",
        Title: "Title",
        Location: "Name",
        Roles: "Name",
    },
    validator: {
        PasswordNotSame: "not same!",
    }
}
