export default {
    entity: 'LimitDef',
    filter: {
    },
    label: {
        Limit: "Limit",
        FreqLimit: "Freq Limit",
        LimitRuleGroup: "LimitRuleGroup",
        Period: {
            Hour: "Hour(s)",
            Day: "Day(s)",
            Sample: "Sample(s)"
        }
    },
    column: {
        LimitType: "Name",
        Description: "Description",
        Prevalence: "Prevalence",
        Deviation: "Deviation",
        SourceLimitDef: "Description",
    },
    editor: {
        LimitType: "Description",
        Description: "Description",
        SourceLimitDef: "Description",
        OccurrenceCount: "OccurrenceCount",
        Period: "Period",
        PeriodCount: "PeriodCount",
        Prevalence: "Prevalence",
        Deviation: "Deviation",
        EmailNotify: "EmailNotify",
        ScreenNotify: "ScreenNotify",
        Reschedule: "Reschedule",
        WorkItem: "Work Items",
    },
    validator:{
        OccurrenceCount:"OccurrenceCount Less than or equal to PeriodCount"
    }
}
