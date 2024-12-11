export default {
    component: {
        component1: {
            Name: "超限",
            Description: "到当前时间，最近的10次超限",
            column: {
                StartDate: "开始日期",
                BarCode: "样本条码",
                SiteName: "采样点",
                TestTypeName: "测试方法",
            }
        },
        component3: {
            Name: "设备超期",
            Description: "已到期的设备和未来30天内将要到期的设备",
            column: {
                Name: "设备",
                EquipmentType: "设备类型",
                CalibrationDate: "校验日期",
                NextCalibrationDate: "下次校验日期",
            }
        },
        component5: {
            Name: "今日任务",
            Description: "今天任务的情况",
            column: {
                UserName: "用户名称",
                Count: "任务数量",
            }
        },
        component7: {
            Name: "我的任务",
            Description: "我的任务信息",
            column: {
                ScheduleDate:"计划日期",
                BarCode:"样本条码",
                TestName:"检测名称",
                TestMethod:"检测类型",
                Site: "采样点名称",
                Location:"位置名称",
            }
        },
    }
}
