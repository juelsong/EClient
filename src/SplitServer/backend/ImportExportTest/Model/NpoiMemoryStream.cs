namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// 
    /// </summary>
    public class NpoiMemoryStream : MemoryStream
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AllowClose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NpoiMemoryStream()
        {
            this.AllowClose = true;
        }

        public override void Close()
        {
            if (this.AllowClose)
            {
                base.Close();
            }
        }
    }
}
