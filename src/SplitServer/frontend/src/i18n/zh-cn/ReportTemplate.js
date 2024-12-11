export default {
    report: {
        compliance: {
            title: '合规性报告',
            desc: '一般是以季度，半年度，年度为单位的多项检测的合规性评估。 主要计算每一种检测项目的偏差发生率及总的偏差率。分级别的偏差率。也包括每个房间的趋势和偏差情况。'
        },
        crr: {
            title: 'CRR报告',
            desc: '报告计算一段时间内的平均恢复率，并比较一个月的恢复情况，以确定是否存在偏移或趋势。'
        },
        classificationSummary: {
            title: '洁净级别分析报告',
            desc: '按洁净级别给出结果的分析，子分类是检测方法或分房间包括阴性，非零，超警戒，超行动的百分比。'
        },
        cumulativeSum: {
            title: '总环境监测趋势分析',
            desc: '选择房间或区域，然后每个检测项目一张趋势图，涵盖所有检测位点，每个位点单独一条曲线。'
        },
        roomSystemAnalysis: {
            title: '房间/区域趋势分析',
            desc: '结果会计算每一种检测方法的所有检测点的最大值，然后按时间画趋势图。还会画拟合曲线。'
        },
        resultsTrending: {
            title: '结果趋势分析',
            desc: '结果趋势报告提供了多个位置内指定日期范围和测试方法的样本结果图表。'
        },
        testResult01: {
            title: '测试结果报告01',
            desc: '测试结果详细信息”报告显示为指定位置、房间/系统和现场采集的所有样本，这些样本都在指定的日期参数范围内。'
        },
        testResult02: {
            title: '测试结果报告02',
            desc: '测试结果详细信息”报告显示为指定位置、房间/系统和现场采集的所有样本，这些样本都在指定的日期参数范围内。'
        },
        testTypeResult: {
            title: '多点位趋势分析报告',
            desc: '结果趋势报告提供了一个房间或位置内指定日期范围和测试方法的样本结果图表。'
        },
        review: {
            title: '空气净化系统监测数据趋势分析',
            desc:'根据某个时间段的检测结果,统计一个房间下的某个测试式方法的某个洁净级别的检测数据及趋势分析,并可计算CPK、置信度等关键质量指标。'
        },
        testResultExcel:{
            title:'检测结果报表',
            desc:"可导出系统在某个时间范围内的检测数据,并可依据客户特定模版对应生成个性化的报表。"
        }
        // emResult: {
        //     title: '菌种鉴定分析图表',
        //     desc: '一般是'
        // },
    },
    label: {
        selected: "已选",
        location: "检测区域",
        site:"采样点",
        testCategory: "检测方法类型",
        testType: "检测方法名称",
        environment: "环境",
        classification: "洁净级别",
        periodType: "数据周期",
        year: "年份",
        period: "周期",
        monthly: "月度",
        quarterly: "季度",
        selectDate: "开始结束时间",
        showParameter: "显示参数",
        auditResult: "批准结束",
        selectError: "请重新确认筛选条件,当前数据为空",
        onlyLimitData: "只显示超限数据",
        showChart: "显示图",
        showForm: "显示表格",
        approved:"是否已批准数据",
        limitType:"限度类别",
        limitDescription:"限度描述",
        limitRuleDescription:"规则描述",
        minPercentage:"区间百分比(%)",
        confidenceLevel:"置信水平(%)",
        toleranceIntervals:"公差区间",
        up:"上限",
        down:"下限",
        CpkLimits:"Cpk",
        chartLimtis:"限度线"
    },
    check: {
        true: "是",
        false: "否"
    }
}