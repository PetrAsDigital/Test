using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpCommunication
{
    public class HttpComm
    {
        public object Execute(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

                request.Method = JSONmethod;    // "GET";
                request.ContentType = JSONContentType;  // "application/json";

                if (JSONRequest != null)
                {
                    string sb = JsonConvert.SerializeObject(JSONRequest);
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }

                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);

                    return objResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
