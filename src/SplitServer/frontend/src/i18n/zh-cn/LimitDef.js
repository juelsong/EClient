export default {
    entity: '检测方法限度',
    filter: {
    },
    label: {
        Limit: "限度",
        FreqLimit: "频率限度",
        LimitRuleGroup: "限度规则",
        Period: {
            Hour: "小时",
            Day: "天",
            Sample: "样本"
        }
    },
    column: {
        LimitType: "限度类别",
        Description: "描述",
        Prevalence: "普遍程度",
        Deviation: "偏差",
        SourceLimitDef: "源限度",
    },
    editor: {
        LimitType: "限度类别",
        Description: "描述",
        SourceLimitDef: "源限度",
        OccurrenceCount: "发生频率",
        Period: "频率单位",
        PeriodCount: "频率区间",
        Prevalence: "普遍程度",
        Deviation: "偏差",
        EmailNotify: "邮件通知",
        ScreenNotify: "屏幕通知",
        Reschedule: "重新计划",
        WorkItem: "工作项目",
    },
    validator:{
        OccurrenceCount:"发生频率小于等于频率区间"
    }
}
