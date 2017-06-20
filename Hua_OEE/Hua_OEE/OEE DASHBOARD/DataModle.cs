using System;
using System.Collections.Generic;

using System.Web;

namespace OEE_DASHBOARD
{
    public class DataModle
    {
        string Machine { get; set; }
        string Product { get; set; }
        int BoardsProduced { get; set; }
        int Failures { get; set; }
        string Runtime { get; set; }
        string OEE { get; set; }
        string Availability { get; set; }
        string Performance { get; set; }
        string Quality { get; set; }
        string PPM { get; set; }
        int BoardsPerPanel { get; set; }
    }
}