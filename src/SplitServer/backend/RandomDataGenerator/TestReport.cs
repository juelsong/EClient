namespace RandomDataGenerator
{
    using System;
    using System.Collections.Generic;

    public enum LimitLevel
    {
        None,
        Alert,
        Action,
        Others
    }
    public class TestReport
    {
        /// <summary>
        /// 报表名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CurrentDateTime { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string DateDescription { get; set; } = string.Empty;

        public string ComponyName { get; set; } = string.Empty;
        public List<Location> LocationsData { get; set; } = new List<Location>();
    }
    public class Location
    {
        public int LocationId { get; set; }
        public string Environment { get; set; } = string.Empty;
        public string Classification { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public List<TestTypeData> TestTypeDatas { get; set; } = new List<TestTypeData>();
    }
    public class TestTypeData
    {
        public int LocationId { get; set; }
        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; } = string.Empty;
        public List<TestData> TestDatas { get; set; } = new List<TestData>(); 
    }
    public class TestData
    {
        public int LocationId { get; set; }
        public int TestTypeId { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public DateTime TestDateTime { get; set; }
        public LimitLevel LimitLevel { get; set; }
        public string LimitDescription { get; set; } = string.Empty;
    }

    public class Meassurement
    {
        public int Id { get; set; }
        public int MeassurementId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}
