using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.ServiceModel;
using Arduino_WCF;
using Arduino_Service.WCFService;
using System.ServiceModel.Description;

namespace Arduino_Service
{
    public partial class Service : ServiceBase
    {
        private Thread t_Listener;
        private bool _CloseAll;
        private SerialPort sp_Arduino;
        internal static ServiceHost ServerHost;
       // private FileStream fs;
       // private StreamWriter sw;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            sp_Arduino = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
            try
            {
                //fs = new FileStream("C:\\Error.txt", FileMode.Create);
                //sw = new StreamWriter(fs);
                sp_Arduino.Open();

                ServerHost = new ServiceHost(typeof(WCF_Windows_Service), new Uri("http://localhost:61142/Server/WCF_Windows_Service.svc"));

                ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
                mBehave.HttpGetEnabled = true;
                ServerHost.Description.Behaviors.Add(mBehave);

                BasicHttpBinding httpb = new BasicHttpBinding();
                ServerHost.AddServiceEndpoint(typeof(Arduino_WCF.IWCF_Windows_Service), httpb, "http://localhost:61142/Server/WCF_Windows_Service.svc");
                ServerHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                ServerHost.Open();

                t_Listener = new Thread(loop);
                t_Listener.Start();
            }
            catch (Exception e)
            {
                /*
                sw.WriteLine(e.Message);
                sw.Close();
                sw.Dispose();
                fs.Dispose();
                */
                if (sp_Arduino.IsOpen)
                    sp_Arduino.Close();
                sp_Arduino.Dispose();
                sp_Arduino = null;
                ServerHost = null;
                Stop();
            }
        }

        protected override void OnStop()
        {
            _CloseAll = true;
            //t_Listener.Abort();
            t_Listener.Join();
            try
            {
                ServerHost.Close();
            }
            catch (Exception e)
            {
                //sw.WriteLine(e.Message);
            }
            ServerHost = null;
            /*
            sw.Close();
            sw.Dispose();
            fs.Dispose();
            */
        }

        private void loop()
        {
            byte[] buffer = new byte[2];
            int arValue = 0;
            WCF_Windows_ServiceClient con = new WCF_Windows_ServiceClient();

            while (!_CloseAll)
            {
                Thread.Sleep(1000);
                //sw.WriteLine("Iteración.");
                try
                {
                    if (sp_Arduino.BytesToRead >= 2)
                    {
                        sp_Arduino.Read(buffer, 0, 2);
                        arValue = (buffer[0] << 8) + buffer[1];
                        con.setCurrentArduinoValue(arValue);
                        //sw.WriteLine(arValue.ToString());
                    }
                    //else
                    //    con.setCurrentArduinoValue(-1);
                }
                catch (Exception e)
                {
                    //sw.WriteLine(e.Message + "\n" + e.InnerException.Message);
                }
            }

            buffer = null;
        }
    }
}
