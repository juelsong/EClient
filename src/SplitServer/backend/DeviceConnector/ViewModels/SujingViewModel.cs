namespace DeviceConnector.ViewModels
{
    using Caliburn.Micro;
    using ESys.EquipmentSync.Device;
    using DeviceConnector.Utilities;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Input;

    internal class SujingViewModel : Screen, INotifyPropertyChanged
    {
        public SujingReader SujingReader { get; set; }
        private string readData;
        private string configData;

        public ICommand ClickConnect { get; private set; }
        public ICommand ClickRead { get; private set; }
        public ICommand ClickReadMulti { get; private set; }
        public ICommand ClickDisConnect { get; private set; }
        public ICommand ClickReadConfig { get; private set; }
        public ICommand ClickReadId { get; private set; }

        private int readNum = 1;

        public int ReadNum
        {
            get { return this.readNum; }

            set
            {
                if (this.readNum != value)
                {
                    this.readNum = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        public string ConfigData
        {
            get { return this.configData; }

            set
            {
                if (this.configData != value)
                {
                    this.configData = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        public string ReadData
        {
            get { return this.readData; }

            set
            {
                if (this.readData != value)
                {
                    this.readData = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SujingViewModel()
        {
            this.ReadData = "No Serial Connect";
            this.ClickConnect = new DelegateCommand(this.ExecuteClickConnect);
            this.ClickRead = new DelegateCommand(this.ExecuteClickRead);
            this.ClickDisConnect = new DelegateCommand(this.ExecuteClickDisConnect);
            this.ClickReadId = new DelegateCommand(this.ExecuteClickReadId);
        }
        private void ExecuteClickReadId(Object obj)
        {
            try
            {
                this.ReadData = "Id:\r\n" + this.SujingReader.GetRecord();
                //this.MetoneReader.Start();
            }
            catch (Exception ee)
            {
                this.ReadData = ee.ToString();
            }
        }
        private void ExecuteClickConnect(Object obj)
        {
            try
            {
                this.SujingReader = new SujingReader("",this.GetXmlData());
                //this.MetoneReader.Start();
                this.ReadData = "CanRead";
            }
            catch (Exception ee)
            {
                this.ReadData = ee.ToString();

            }


            //this.ConfigData = this.MetoneReader.GetIDBlock().GetFormattedString();
        }
        private void ExecuteClickDisConnect(Object obj)
        {
            this.ReadData = "No Serial Connect";

            //this.ConfigData = this.MetoneReader.GetIDBlock().GetFormattedString();
        }
        private void ExecuteClickRead(Object obj)
        {
            if (this.SujingReader is not null )
            {
                this.ReadData = "Wait....";
                this.ReadData = "Config:\r\n" + this.SujingReader.GetRecord();

            }
            else
            {
                this.ReadData = "No Serial Connect";

            }
        }
        private string GetXmlData()
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@".\config.xml");
            //XmlNode deviceNode = doc.SelectSingleNode("/Device");
            //if (deviceNode != null)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    XmlNode deviceNameNode = deviceNode.SelectSingleNode("DeviceName");
            //    if (deviceNameNode != null)
            //    {
            //        sb.Append(deviceNameNode.OuterXml);
            //        XmlNodeList allNodesAfterDeviceName = deviceNode.SelectNodes("*[position() > count(../DeviceName)]");
            //        foreach (XmlNode node in allNodesAfterDeviceName)
            //        {
            //            sb.Append(node.OuterXml);
            //        }
            //    }
            //    string xmlContent = sb.ToString();
            //    Console.WriteLine(xmlContent);
            //}
            return File.ReadAllText(@".\config.xml");

        }
    }
}
