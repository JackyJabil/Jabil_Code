using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluorineFx;
using FluorineFx.Net;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.AMF3;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace OEE_DASHBOARD
{
    /// <summary>
    /// Summary description for GetmachineOEE
    /// </summary>
    public class GetmachineOEE : IHttpHandler
    {
        static bool ReadData = true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ReadData = true;
            string id = context.Request.QueryString["id"].ToUpper().Replace("=", "").Replace("I", "").Replace("D", "").Trim();
            if (id == null)
            {
                return;
            }
            if (id == "")
            {
                return;
            }
            int ID = Convert.ToInt16(id);
          
            string gatewayURL = System.Web.Configuration.WebConfigurationManager.AppSettings["gatewayURL"];
            string myServer = System.Web.Configuration.WebConfigurationManager.AppSettings["myServer"];
            string equipIDs = System.Web.Configuration.WebConfigurationManager.AppSettings["equipID"];
            string[] equipID = equipIDs.Split(',');
           
            NetConnection _netConnection;
            Console.WriteLine(string.Format("Try to connect Server "));
            _netConnection = new NetConnection();
            _netConnection.ObjectEncoding = ObjectEncoding.AMF3;
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect(gatewayURL);
            bool first = true;
            while (ReadData)
            {
                if (!first)
                {
                    System.Threading.Thread.Sleep(50);
                }
                else
                {
                    first = false;
                    _netConnection.Call("my-amf", "net", "CAMXMachineAllModulesState", "getCAMXdata", new RemoteHandler(context), new Object[] { myServer, Convert.ToInt32(equipID[ID]), 0, GetShiftStartTime().ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                }
            }
            _netConnection.close();
           
        }

        static DateTime GetShiftStartTime()
        {
            DateTime t = DateTime.Now;
            if (t.Hour >= 7 && t.Hour < 15)
            {
                t = t.Date + new TimeSpan(7, 0, 0);
                return t;
            }
            if (t.Hour >= 15 && t.Hour < 23)
            {
                t = t.Date + new TimeSpan(15, 0, 0);
                return t;
            }
            if (t.Hour >= 23)
            {
                t = t.Date + new TimeSpan(23, 0, 0);
                return t;
            }
            if (t.Hour < 7)
            {
                t = t.Date.AddDays(-1) + new TimeSpan(23, 0, 0);
                return t;
            }
            return t;


        }
        
        private class RemoteHandler : IPendingServiceCallback
        {
            HttpContext context;
            public RemoteHandler(HttpContext _context)
            {
                context = _context;
            }
            public void ResultReceived(IPendingServiceCall call)
            {
                ReadData = false;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //string json = jss.Serialize(call.Result);

                //    string jsondata = "{\"page\":\"1\"," +
                //"      \"total\":1," +
                //"      \"records\":\"8\"," +
                //"      \"rows\":" + json + "}";
                JQGridResults result = new JQGridResults();
                List<JQGridRow> rows = new List<JQGridRow>();

                Object[] list = call.Result as Object[];
                int i = 0;
                foreach (var item in list)
                {
                    JQGridRow rowjs = new JQGridRow();
                    rowjs.id = i++;
                    rowjs.cell = new string[5];
                    FluorineFx.ASObject aa = item as FluorineFx.ASObject;
                    Object ob1, ob2, ob3, ob4, ob5;
                    aa.TryGetValue("Equipment_ID", out ob1);
                    aa.TryGetValue("Module_ID", out ob2);
                    aa.TryGetValue("State", out ob3);
                    aa.TryGetValue("Percent", out ob4);
                    aa.TryGetValue("Time", out ob5);


                    rowjs.cell[0] = Convert.ToString(ob1);
                    rowjs.cell[1] = Convert.ToString(ob2);
                    rowjs.cell[2] = Convert.ToString(ob3);
                    rowjs.cell[3] = Convert.ToString(ob4);
                    rowjs.cell[4] = Convert.ToString(ob5);
                    rows.Add(rowjs);                       
                }
                result.rows = rows.ToArray();
                result.page = 1;
                result.total =i;
                result.records = i;
                string jsondata= new JavaScriptSerializer().Serialize(result);
                context.Response.Write(jsondata);

                //var cc = list[0];
                //string bb = "";
                //bb = bb;
                //      foreach (var o in call.Result)
                //{
                //    JQGridRow row = new JQGridRow();


                //}
                //  context.Response.Write(jsondata);
            }
        }



        public struct JQGridResults
        {
            public int page;
            public int total;
            public int records;
            public JQGridRow[] rows;

        }

        public struct JQGridRow
        {
            public int id;
            public string[] cell;
        }


         void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            //string level = e.Info["level"] as string;
            //if (level == "error")
            //{
            //    System.Console.WriteLine("Error: " + e.Info["code"] as string);
            //}
            //if (level == "status")
            //{
            //    System.Console.WriteLine("Status: " + e.Info["code"] as string);
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}