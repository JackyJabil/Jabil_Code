using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluorineFx;
using FluorineFx.Net;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.AMF3;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;

namespace GetCamxDataconsole
{
    class Program
    {
        static string gatewayURL = "";
        static string myServer = "";
        static string equipIDs = "";
        static int sleeptime=0;

       
        static List<Object> ob = new List<Object>();
        static int MachineCount = 0;
        static int num = 0;

        static bool Taskfinish = false;
        static bool AllTaskfinish = false;

        static void Main(string[] args)
        {
             gatewayURL = ConfigurationManager.AppSettings["gatewayURL"].Trim();
             myServer = ConfigurationManager.AppSettings["myServer"].Trim();
             equipIDs = ConfigurationManager.AppSettings["equipID"].Trim();
             sleeptime = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTime"].Trim());
           
            Console.WriteLine(string.Format("{0}"+gatewayURL,"gatewayURL:"));
            Console.WriteLine(string.Format("{0}"+myServer,"myServer:"));
            Console.WriteLine(string.Format("{0}"+equipIDs,"equipID:"));
            Console.WriteLine(string.Format("{0}" + sleeptime.ToString(), "sleeptime:"));
            string[] equipID = equipIDs.Split(',');
            for (int i = 0; i < equipID.Length; i++)
            {
                equipID[i] = equipID[i].Trim();               
            }
            MachineCount = equipID.Length;

            Console.WriteLine(string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Maching Count：{0}", MachineCount));
            NetConnection _netConnection;
            Console.WriteLine(string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Try to connect Server ")); 
            _netConnection = new NetConnection();
            _netConnection.ObjectEncoding = ObjectEncoding.AMF3;
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect(gatewayURL);
            while (true)
            {
                if (!AllTaskfinish)
                {                  
                    System.Threading.Thread.Sleep(1000);
                    if ((!Taskfinish) && (!AllTaskfinish))
                    {
                        Taskfinish = true;
                        if (num == MachineCount)
                        {
                            AllTaskfinish = true;
                            ob.Clear();

                        }
                        else
                        {
                            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Downloading " + (num + 1).ToString() + " number,equipID:" + equipID[num]);
                            //_netConnection.Call("my-amf", "net", "PortalCAMXGetMachineOEE", "getData", new RemoteHandler(), new Object[] { myServer, int.Parse(equipID[num]), 0, GetShiftStartTime().ToString("yyyy-MM-dd HH:mm:ss"), null, 0 });

                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Downloading " + (num + 1).ToString() + " number,equipID:" + equipID[num]);
                            _netConnection.Call("my-amf", "net", "PortalCAMXGetMachineOEEByLane", "getData", new RemoteHandler(), new Object[] { myServer, int.Parse(equipID[num]), 0, GetShiftStartTime().ToString("yyyy-MM-dd HH:mm:ss"), null, 1, "Lane1" });
                            
                        }
                    }                   
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Wating....");
                    System.Threading.Thread.Sleep(sleeptime*1000);
                    AllTaskfinish = false;
                    num = 0;
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Downloading....");              
                }
            }

           

        }

         static DateTime GetShiftStartTime()
        {
            DateTime t = DateTime.Now;
            if (t.Hour >= 7 && t.Hour < 15)
            {
                t=t.Date+ new TimeSpan(7,0,0);
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
            if (t.Hour <7)
            {
                t = t.Date.AddDays(-1) + new TimeSpan(23, 0, 0);
                return t;
            }
            return t;
         
          
        }

        static void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = e.Info["level"] as string;
            if (level == "error")
            {
                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Error: " + e.Info["code"] as string);
            }
            if (level == "status")
            {
                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Status: " + e.Info["code"] as string);
            }
        }

         public class RemoteHandler : IPendingServiceCallback
        {
            public void ResultReceived(IPendingServiceCall call)
            {
               
                object result = call.Result;
                ob.Add(result);               
              
                if (ob.Count == MachineCount)
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string json = "{\"OEEData\":[" + jss.Serialize(ob).Replace("[", "").Replace("]", "").Trim() + "]}";
                    using (StreamWriter sw = File.CreateText(System.AppDomain.CurrentDomain.BaseDirectory + "json_hua.txt"))
                    {
                        sw.WriteLine(json);
                    }
                    AllTaskfinish = true;
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "Had finish：" + (num + 1).ToString() + " number");                  
                    num = 0;
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "===> " + "All data download finish!");
                    ob.Clear();
                }
                else
                {
                    Console.WriteLine("Had finish：" + (num + 1).ToString() + "number");
                    num++;
                }
                Taskfinish = false;
               
               
            }
        }
    }
}
