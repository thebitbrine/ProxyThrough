using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TheBitBrine
{
    internal class UsefulMethods
    {
        public bool Empty(object Stuff)
        {
            if (Stuff == "")
                return true;
            if (Stuff == null)
                return true;
            if (Stuff.ToString() == "")
                return true;
            return false;
        }

        public List<string> GetLinksRegex(string message)
        {
            List<string> list = new List<string>();

            if (!Empty(message))
            {
                Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);

                MatchCollection matches = urlRx.Matches(message);
                foreach (Match match in matches)
                {
                    list.Add(match.Value);
                }
                return list;
            }

            return list;
        }

        public string HrefToFullLink(string ParentLink, string HTMLContent)
        {
            if (!Empty(ParentLink) && !Empty(HTMLContent))
            {
                string HTMLContentWithTheMeat = "";
                string[] HTMLContentLines = HTMLContent.Split('\n');
                for (int i = 0; i < HTMLContentLines.Length; i++)
                {
                    try
                    {
                        //string TheFatMeat = GetBetween(HTMLContentLines[i], "<a href=\"", "\">");
                        string TheMeat = GetBetween(HTMLContentLines[i], "<a href=\"", "\">"); /*GetBetween(TheFatMeat, ">", "<");*/
                        string TheHalfFatMeat = "<a href=\"" + TheMeat + "\">" + TheMeat + "</a>";
                        string TheBBQ = "";

                        if (ParentLink.EndsWith("/") == true) TheBBQ = "<a href=\"" + ParentLink + TheMeat + "\">" + TheMeat + "</a>";
                        else TheBBQ = "<a href=\"" + ParentLink + "/" + TheMeat + "\">" + TheMeat + "</a>";

                        if (TheMeat != "" && TheMeat != "../" && TheMeat != "..")
                            HTMLContentWithTheMeat += TheBBQ;
                        else
                            HTMLContentWithTheMeat += "";
                    }
                    catch
                    {
                        HTMLContentWithTheMeat += "";
                    }
                }
                return HTMLContentWithTheMeat;
            }
            else
            {
                return null;
            }
        }

        public string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public List<string> DirSearch(string sDir)
        {
            List<string> List = new List<string>();
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        string xzc = f.Replace('\\', ';');
                        List.Add(xzc);
                    }
                    DirSearch(d);
                }
                return List;
            }
            catch (System.Exception excpt)
            {
                List.Add(excpt.Message);
                return List;
            }
        }

        public string LinkToHTML(string Link)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                try
                {
                    return webClient.DownloadString(Link);
                }
                catch (Exception ex)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            return webClient.DownloadString(Link);
                            Thread.Sleep(250);
                        }
                        catch
                        {
                        }
                    }
                    MessageBox.Show("Invaild or unreachable host. Check the link to make sure it's up and valid.\r\nStats for nerds: \r\n" + ex.Message, "Something's Wrong", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                return webClient.DownloadString(Link);
            }
        }

        public bool VaildateLink(string Link)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadString(Link);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool HTMLOrNot(string Link)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);
                request.Method = "GET";
                request.ServicePoint.Expect100Continue = false;
                //request.ContentType = "application/x-www-form-urlencoded";

                using (WebResponse response = request.GetResponse())
                {
                    if (response.ContentType == "text/html")
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    public class AppSettingsRW
    {
        //public bool Encrypt = false;
        ///<summary>
        ///<para>Specify the Keyname and KeyValue, and it will be saved as a file called app.settings under the execution location. If Overwrite is set to True it will overwrite the whole file, Otherwise it will add the new vaule to the file if it doesn't exist.</para>
        ///</summary>
        public void AppSettings(string KeyName, string KeyValue, bool Overwrite)
        {
        }

        ///<summary>
        ///<para>Specify the Keyname, KeyValue, File Name and Path, and it will be saved in a file in the specified path (If the given path isn't absolute, it will save the file at the execution location and will use the string given as path as the name of the file). If Overwrite is set to True it will overwrite the whole file, Otherwise it will add the new vaule to the file if it doesn't exist.</para>
        ///</summary>
        public void AppSettings(string KeyName, string KeyValue, string Path, bool Overwrite)
        {
        }

        ///<summary>
        ///<para>Specify the Keyname*, KeyValue*, File Name* and Path*, and it will return a bool determining whether the given data exists or not.</para>
        ///<para>If File Name and Path not specified it will look through app.settings at the execution location. At least one of KeyName or KeyValue must be specified and with the given data it will return a bool determining whether the given string exists in the whole file (given path if specified) or not.</para>
        ///</summary>
        public bool AppSettingsExist(string KeyName, string KeyValue, string Path, bool Overwrite)
        {
            return false;
        }

        ///<summary>
        ///<para>Specify the Keyname, KeyValue, File Name and Path, and it will return the string from with the given KeyName from a file called app.settings at the execution location.</para>
        ///</summary>
        public string AppSettings(string KeyName)
        {
            return null;
        }

        //private string[] AppSettingsEngine(string[] Values)
        //{
        //    TheBitBrine.UsefulMethods V = new UsefulMethods();
        //    string KeyName = Values[0];
        //    string KeyValue = Values[1];
        //    string Path = Values[2];
        //    bool Overwrite = bool.Parse(Values[3]);
        //    bool Read = bool.Parse(Values[4]);
        //    bool ReturnRawData = bool.Parse(Values[5]);

        //    string RawData = File.ReadAllText(Path);

        //    if (Read == true)
        //    {
        //        if (ReturnRawData == true)
        //        {
        //            string[] ReturnData = { RawData };
        //            return ReturnData;
        //        }
        //        else
        //        {
        //            string[] ReturnArray = Path.Split('\n');
        //        }
        //    }
        //    else
        //    {
        //        if()
        //    }

        //}
    }
}