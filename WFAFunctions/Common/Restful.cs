using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WFAFunctions.Common
{
    public static class Restful
    {
        public static string GET(string url)
        {
            HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(url);
            string result;
            using (Stream responseStream = httpWebReq.GetResponse().GetResponseStream())
            {
                result = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
            }
            return result;
        }

        public static string POST(string url, string jsonContent)
        {
            string result;

            string text = "";
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "";
                text = webClient.UploadString(url, null, jsonContent);
            }
            result = text;
            return result;
        }
    }
}
