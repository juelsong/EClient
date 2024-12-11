export default {
    report: {
        compliance: {
            title: 'Compliance Report',
            desc: 'The Compliance Report summarizes the amount of samples taken and their corresponding excursion rates over a period of time. Additionally, the report includes historized data for previous time frames and provides excursion information and microbial recoveries for the selected period of time.'
        },
        crr: {
            title: 'CRR',
            desc: 'The Critical Recovery Rate report calculates the average recovery rate for a duration of time and compares a months recovery to establish if there is an excursion or trend.'
        },
        classificationSummary: {
            title: 'Classification Summary',
            desc: 'The Classification Summary provides the user with a tabular summary of sampling activity by site classification, then by a configurable choice of subgroups (e.g., room, test method) and time interval (e.g., daily, weekly, monthly, quarterly, etc).  Results are summarized by result status (i.e., positive non-zero, negative, alert, action, etc.).'
        },
        cumulativeSum: {
            title: 'Cumulative Sum Report',
            desc: 'The Cumulative Sum Report takes into account the normal performance of a dataset and cumulatively sums the deviations from that performance. If the deviations from the performance are equally positive and negative, the data is shown to have a neutral disposition. If there is a shift from the normal performance, it will be visualized within the included graphs.'
        },
        roomSystemAnalysis: {
            title: 'Trend By Room/System',
            desc: 'The Trend by Room/System provides the user with a trend chart displaying trend lines for the results aggregate of each test method selected for a room.'
        },
        resultsTrending: {
            title: 'Result Trending',
            desc: 'The Result Trending report provides a chart of sample results within a room or location for a specified date range and test method.'
        },
        testResult01: {
            title: 'Test Result Details 01',
            desc: 'The Test Result Details report displays all samples that were taken for the specified Location(s), Room/System(s), and Site(s),  and fall within the specified date parameters.'
        },
        testResult02: {
            title: 'Test Result Details 02',
            desc: 'The Test Result Details report displays all samples that were taken for the specified Location(s), Room/System(s), and Site(s),  and fall within the specified date parameters.'
        },
        testTypeResult: {
            title: 'Multi TestTypeResult',
            desc: 'The Result Trending report provides a chart of sample results within multi room or location for a specified date range and test method.'
        },
        review: {
            title: 'review',
            desc: 'review'
        }
        // emResult: {
        //     title: '菌种鉴定分析图表',
        //     desc: '一般是'
        // },
    },
    label: {
        location: "location",
        site: "site",
        testCategory: "testCategory",
        testType: "testType",
        environment: "environment",
        classification: "classification",
        periodType: "periodType",
        year: "year",
        period: "period",
        monthly: "monthly",
        quarterly: "quarterly",
        selectDate: "selectDate",
        showParameter: "showParameter",
        auditResult: "auditResult",
        selectError: "Please reconfirm the filter criteria, the current data is empty",
        onlyLimitData: "Display only overrun data",
        showChart: "Show Chart",
        showForm: "Show Form",
        approved: "Approved",
        limitType:"limitType",
        limitDescription:"limitDescription",
        limitRuleDescription:"limitRuleDescription",
        MinPercentage:"MinPercentage",
        confidenceLevel:"ConfidenceLevel",
        toleranceIntervals:"ToleranceIntervals",
        up:"up",
        down:"down",
        cpk:"Cpk",
        chartLimtis:"chartLimtis"
    },
    check: {
        true: "true",
        false: "false"
    }
}