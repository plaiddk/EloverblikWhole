using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Text;
using static ConsoleApp6.recorddatatableconverter;

namespace ConsoleApp6
{
    class spotprice
    {

        public static void getSpotPrice(string date)
        {

            //Get hourly prices
            string url = $"https://api.energidataservice.dk/datastore_search_sql?sql=SELECT * from \"elspotprices\" where \"PriceArea\"='DK1' and \"HourDK\">'{date}'";
            HttpClient hourprice = new HttpClient();
            using (HttpResponseMessage response = hourprice.GetAsync(url).Result)
            using (HttpContent content = response.Content)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = content.ReadAsStringAsync().Result;
                    File.WriteAllText(@"D:\testdata\jsonpriceshourly.json", json);

                    JObject tmp = JObject.Parse(json);

                    string jsonhourly = tmp["result"].ToString();



                    var settings = new JsonSerializerSettings
                    {
                        Converters = new[] { new RecordDataTableConverter() },
                    };

                    var table = JsonConvert.DeserializeObject<DataTable>(jsonhourly, settings);


                    using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=FuckingNice; Integrated Security=True"))
                    {
                        using (SqlBulkCopy bulk = new SqlBulkCopy(conn))
                        {

                            bulk.DestinationTableName = "dbo.spotprices";
                            conn.Open();

                            bulk.WriteToServer(table);
                            conn.Close();


                            //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonhourly);

                            //DataTable dataTable = dataSet.Tables["Table1"];

                        }
                    }
                }
            }
        }
    }
}
