namespace EConsole.Model
{
    public class Environment
    {
        public string token { get; set; }
        public string deviceNo { get; set; }
        public string superviseNumber { get; set; }
        public string deviceDesc { get; set; }
        public double pm10 { get; set; }
        public double pm25 { get; set; }
        public double tsp { get; set; }
        public double noise { get; set; }
        public double temp { get; set; }
        public string windDirection { get; set; }
        public double windSpeed { get; set; }
        public string recordTime { get; set; }
        public double airPressure { get; set; }
        public double co { get; set; }
        public double h2s { get; set; }
        public double humidity { get; set; }
        public double nh3 { get; set; }
        public double no2 { get; set; }
        public double so2 { get; set; }
        public double voc { get; set; }
    }
}
