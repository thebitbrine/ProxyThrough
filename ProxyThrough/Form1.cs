using BrightIdeasSoftware;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TheBitBrine;

namespace ProxyThrough
{
    public partial class Form1 : Form
    {
        #region Fields

        public const int INTERNET_OPTION_REFRESH = 37;
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;

        public bool Connected = false;
        public bool ListAvailable = false;
        public bool PingDone = false;
        public List<ProxyItem> ProxyList = new List<ProxyItem>();
        public List<string> RawList = new List<string>();
        public bool ReadyToPopulate = false;
        public string TempPath = System.IO.Path.GetTempPath() + "ProxyThroughList.json";
        public string UIStatus = "Ready...";
        private static bool settingsReturn, refreshReturn;
        private UsefulMethods UM = new UsefulMethods();
        

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
            ProxyListView.Columns.Add(new OLVColumn(title: "IP Address", aspect: "IPAddress"));
            ProxyListView.Columns.Add(new OLVColumn(title: "Port", aspect: "Port"));
            ProxyListView.Columns.Add(new OLVColumn(title: "Country", aspect: "Country"));
            ProxyListView.Columns.Add(new OLVColumn(title: "Anonymity", aspect: "Anonymity"));
            ProxyListView.Columns.Add(new OLVColumn(title: "SSL", aspect: "SSL"));
            ProxyListView.Columns.Add(new OLVColumn(title: "Google Passed", aspect: "GooglePassed"));
            ProxyListView.Columns.Add(new OLVColumn(title: "Response Time", aspect: "Ping"));

            //Disconnects From Potential Proxy
            ConnectToProxy("OFF");

            if (File.Exists(TempPath) == true && string.IsNullOrWhiteSpace(File.ReadAllText(TempPath)) == false)
            {
                ProxyList = JSONToObject(File.ReadAllText(TempPath));
                PopulateListView();
                ProxyListView.Sort(new OLVColumn(title: "Ping", aspect: "Ping"), SortOrder.Ascending);
            }
            else
            {
                UpdateListThreaded();
            }
        }

        #endregion Constructors

        #region Methods

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public bool BasicListValidation(string RawString)
        {
            //The If statment had to be writen in this format because it was getting too long to all of it be visiable at the same time just like this comment but only longer.
            bool isValid = false;
            if (RawString.Contains("Proxy list updated at"))
                isValid = true;
            if (RawString.Contains("http://spys.me/proxy.txt"))
                isValid = true;
            if (RawString.Contains("https://t.me/spys_one"))
                isValid = true;
            if (RawString.Contains("https://twitter.com/spys_one"))
                isValid = true;
            if (RawString.Contains("IP address:Port"))
                isValid = true;
            if (RawString.Contains("Text format"))
                isValid = true;

            return isValid;
        }

        public void ConnectToProxy(string HostPort)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            if (HostPort != "OFF")
            {
                registry.SetValue("ProxyEnable", 1);
                registry.SetValue("ProxyServer", HostPort);
            }
            else
            {
                registry.SetValue("ProxyEnable", 0);
                registry.SetValue("ProxyServer", "");
            }

            // These lines implement the Interface in the beginning of program
            // They cause the OS to refresh the settings, causing IP to realy update
            settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        public List<ProxyItem> JSONToObject(string SerializedJSON)
        {
            return (List<ProxyItem>)JsonConvert.DeserializeObject(SerializedJSON, typeof(List<ProxyItem>));
        }

        public bool ListChanged(object List, string Path)
        {
            string TempList = File.ReadAllText(Path);
            string ListFromFile = ObjectToJSON(List);
            if (TempList != ListFromFile)
                return true;
            else
                return false;
        }

        public void ListToFile()
        {
            File.WriteAllText(System.IO.Path.GetTempPath() + "ProxyThroughList.json", ObjectToJSON(ProxyList));
        }

        public string ObjectToJSON(object Object)
        {
            return JsonConvert.SerializeObject(ProxyList);
        }

        public void PingAll()
        {
            try
            {
                PingDone = false;
                UIStatus = "Benchmarking Proxy Hosts...";
                for (int i = 0; i < ProxyList.Count; i++)
                {
                    if (PingDone == true)
                        break;
                    UIStatus = "Benchmarking Proxy Hosts (" + (i + 1) + "/" + ProxyList.Count + ")";

                    ConnectToProxy(ProxyList[i].IPAddress + ":" + ProxyList[i].Port);
                    Thread.Sleep(150);

                    Stopwatch SW = new Stopwatch();
                    PingReply reply = null;
                    Ping pinger = new Ping();
                    try
                    {
                        reply = pinger.Send("8.8.8.8", 300);
                    }
                    catch
                    {
                        // Discard PingExceptions and return false;
                    }
                    if (reply.Status == IPStatus.TimedOut)
                    {
                        ProxyList[i].Ping = "-Dead-";
                    }
                    else
                    {
                        SW.Start();
                        try
                        {
                            new TimedWebClient { Timeout = int.Parse(reply.RoundtripTime + 500 + "") }.DownloadString(new Uri("http://spys.me/").AbsoluteUri);
                            ProxyList[i].Ping = SW.ElapsedMilliseconds + " ms";
                        }
                        catch
                        {
                            ProxyList[i].Ping = "-Dead-";
                        }
                        SW.Stop();
                    }

                    ProxyListView.ClearObjects();
                    ProxyListView.SetObjects(ProxyList);
                }
            }
            catch { }

            ConnectToProxy("OFF");

            PingDone = true;

            UIStatus = "Saving the list into local disk...";
            //Save the list with pings into a serialized json file in temp directory
            ListToFile();

            UIStatus = "Ready...";
        }

        public void PingAllThreaded()
        {
            ThreadStart PingAllTS = new ThreadStart(PingAll);
            Thread PingAllTH = new Thread(PingAllTS);
            PingAllTH.IsBackground = true;
            PingAllTH.Start();
        }

        public void PopulateListView()
        {
            UIStatus = "Populating List...";

            //for (int i = 0; i < ProxyList.Count; i++)
            //{
            //    ProxyListView.Items.Add(new OLVListItem(new string[] { ProxyList[i].IPAddress, ProxyList[i].Port, ProxyList[i].Country, ProxyList[i].Anonymity, ProxyList[i].SSL+"", ProxyList[i].GooglePassed+"" }));
            //}

            ProxyListView.SetObjects(ProxyList);

            foreach (ColumnHeader column in ProxyListView.Columns)
            {
                column.Width = -2;
                column.TextAlign = HorizontalAlignment.Center;
            }

            UIStatus = "Ready...";
        }

        public void RenderItems()
        {
            UIStatus = "Processing Proxy List...";

            //Find IP Address
            for (int i = 0; i < RawList.Count; i++)
            {
                string TempIP;
                string TempPort;
                string TempCountry;
                string TempAdditionInfo;
                string TempAnonymity;
                string TempSSL;
                string TempGooglePassed;

                string[] TempAdditionInfoArray = new string[5];

                if (string.IsNullOrWhiteSpace(RawList[i]) == false && RawList[i].Contains(":") && UM.ValidateIPv4(RawList[i].Substring(0, RawList[i].IndexOf(":", 0) - 0)) == true)
                {
                    TempIP = RawList[i].Substring(0, RawList[i].IndexOf(":", 0) - 0);
                    TempPort = UM.GetBetween(RawList[i], ":", " ");
                    TempCountry = UM.GetBetween(RawList[i], TempPort + " ", "-");
                    TempAdditionInfo = RawList[i].Replace(TempIP + ":" + TempPort + " ", "");

                    TempCountry = TempAdditionInfo.Substring(0, 2);
                    TempAdditionInfo = TempAdditionInfo.Replace(TempCountry + "-", "").Replace(" ", "");
                    if (TempAdditionInfo.EndsWith("+") == true) { TempGooglePassed = "+"; } else { TempGooglePassed = " "; }
                    TempAdditionInfoArray = TempAdditionInfo.Split('-');
                    TempAnonymity = TempAdditionInfoArray[0];
                    if (TempAdditionInfoArray.Length > 1 && string.IsNullOrWhiteSpace(TempAdditionInfoArray[1]) == false)
                        TempSSL = "+";
                    else
                        TempSSL = " ";

                    //Finally add it list
                    ProxyList.Add(new ProxyItem { IPAddress = TempIP, Port = TempPort, Country = TempCountry, Anonymity = TempAnonymity, SSL = TempSSL, GooglePassed = TempGooglePassed, Ping = "N/A" });
                }
            }

            //PingAll();

            ReadyToPopulate = true;
        }

        public void UpdateList()
        {
            try
            {
                UIStatus = "Fetching Proxy List...";

                ReadyToPopulate = false;
                string contents;
                using (var wc = new System.Net.WebClient())
                {
                    try
                    {
                        contents = wc.DownloadString("http://bit.ly/2Ale2pP");
                    }
                    catch
                    {
                        contents = null;
                    }
                    if (contents == null || BasicListValidation(contents) == false)
                    {
                        contents = wc.DownloadString("http://bit.ly/2AnAbDK");
                        if (BasicListValidation(contents) == false)
                            ListAvailable = false;
                        else
                            ListAvailable = true;
                    }
                    else
                        ListAvailable = true;
                }

                if (ListAvailable == true)
                {
                    RawList = contents.Split('\n').ToList<string>();
                }
            }
            catch
            {
                MessageBox.Show("Check internet connectivity, and try to refresh the list.", "ProxyThrough - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            RenderItems();
        }

        public void UpdateListThreaded()
        {
            ThreadStart ListTS = new ThreadStart(UpdateList);
            Thread ListTH = new Thread(ListTS);
            ListTH.IsBackground = true; //To avoid the thread from running in the background after exit.
            ListTH.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToProxy("OFF");
            Connected = false;
            ProxyListView.Sort(new OLVColumn(title: "Ping", aspect: "Ping"), SortOrder.Ascending);
            PingAllThreaded();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (Connected == false && ProxyListView.SelectedObject != null && string.IsNullOrWhiteSpace(((ProxyItem)ProxyListView.SelectedObject).IPAddress) == false)
            {
                PingDone = true;
                ConnectToProxy(((ProxyItem)ProxyListView.SelectedObject).IPAddress + ":" + ((ProxyItem)ProxyListView.SelectedObject).Port);
                Connected = true;
            }
            else
            {
                ConnectToProxy("OFF");
                Connected = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ConnectToProxy("OFF");
            MessageBox.Show("Proxy Settings Cleared Successfully.", "ProxyThrough - CPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Level 2
        private void ListPopulatorTimer_Tick(object sender, EventArgs e)
        {
            if (ReadyToPopulate == true)
            {
                PopulateListView();
                ReadyToPopulate = false;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ProxyListView.ClearObjects();
            ProxyList.Clear();
            UpdateListThreaded();
            ProxyListView.SetObjects(ProxyList);
        }

        private void StatusUpdater_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = UIStatus;
            if (PingDone == true)
            {
                ProxyListView.ClearObjects();
                ProxyListView.SetObjects(ProxyList);
                PingDone = false;
            }
            if (Connected == true)
            {
                SetProxyButton.Text = "Disconnect";
            }
            else
            {
                SetProxyButton.Text = "Set Proxy";
            }
        }
        
        private void GithubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/thebitbrine/ProxyThrough/");
        }

        #endregion Methods

        #region Classes


        public class ProxyItem
        {
            #region Fields

            public string Anonymity;
            public string Country;
            public string GooglePassed;
            public string IPAddress;
            public string Ping;
            public string Port;
            public string SSL;

            #endregion Fields
        }

        public class TimedWebClient : WebClient
        {
            #region Constructors

            public TimedWebClient()
            {
                this.Timeout = 500;
            }

            #endregion Constructors

            #region Properties

            // Timeout in milliseconds, default = 600,000 msec
            public int Timeout { get; set; }

            #endregion Properties

            #region Methods

            protected override WebRequest GetWebRequest(Uri address)
            {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;
                return objWebRequest;
            }

            #endregion Methods
        }

        #endregion Classes
    }
}