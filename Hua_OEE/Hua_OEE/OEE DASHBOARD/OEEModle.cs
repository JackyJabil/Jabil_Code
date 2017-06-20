using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OEE_DASHBOARD
{
    public class OEEModle
    {
        public int Equipment_ID { set; get; }
        public int Module_ID { set; get; }
        public string State { set; get; }
        public string Percent { set; get; }
        public string Time { set; get; }
    }
}