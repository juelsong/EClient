export default {
    component: {
        component1: {
            Name: "10 Most Recent Excrusions",
            Description: "By Sample StartDate",
            column: {
                StartDate: "StartDate",
                BarCode: "BarCode",
                SiteName: "SiteName",
                TestTypeName: "TestTypeName",
            }
        },
        component3: {
            Name: "Equipment Approching Calibration Due Date",
            Description: "Within Next 30 Days",
            column: {
                Name: "Equipment",
                EquipmentType: "EquipmentType",
                CalibrationDate: "CalibrationDate",
                NextCalibrationDate: "NextCalibrationDate",
            }
        },
        component5: {
            Name: "Today Work",
            Description: "Today Work",
            column: {
                UserName: "UserName",
                Count: "Count",
            }
        },
        component7: {
            Name: "My Work",
            Description: "My Work",
            column: {
                ScheduleDate:"ScheduleDate",
                BarCode:"BarCode",
                TestName:"TestName",
                TestMethod:"TestMethod",
                Site: "Site",
                Location:"Location",
            }
        },
    }
}
