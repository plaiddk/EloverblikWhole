using Newtonsoft.Json;
using System.IO;

namespace ConsoleApp6
{
  public  class jsonaccess
    {

        public static JsonTable FetchJson(string path)
        {


            JsonTable j = JsonConvert.DeserializeObject<JsonTable>(path);

            return j;

        }
    }
}
