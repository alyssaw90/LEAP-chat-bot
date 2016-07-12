using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Bot_Application1
{
    public class LUISTypeParser
    {
        public static async Task<LUISParser> ParseUserInput(string strInput)
        {
            string strRet = string.Empty;
            string strEscaped = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                string uri = "https://api.projectoxford.ai/luis/v1/application?id=e30d91cd-2e0b-4c5b-9203-8ab811be1985&subscription-key=4f3d135e368c48ae85ecdbb398fc4199&q=" + strEscaped;
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<LUISParser>(jsonResponse);
                    return _Data;
                }
            }
            return null;
        }
    }

    public class LUISParser
    {
        public string query { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }
    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }
}