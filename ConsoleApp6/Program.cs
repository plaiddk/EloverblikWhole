using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data;

using MoreLinq;
using System.ComponentModel;
using System.Xml;
using System.Data.SqlClient;
using System.Threading;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using System.Globalization;

namespace ConsoleApp6
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            //Delete null in spotprices
            var delete = SqlStoredProcHelper.ExecuteProc(@"Data Source =.\SQLEXPRESS; Initial Catalog = FuckingNice; Integrated Security = True", "delete from dbo.spotprices where SpotPriceDKK is null");
            //get spot prices           
            CultureInfo provider = CultureInfo.InvariantCulture;

            string conn = @"Data Source =.\SQLEXPRESS; Initial Catalog = FuckingNice; Integrated Security = True";
            string query = "Select max([HourUTC]) as MaxDate from dbo.spotprices where SpotPriceDKK is not null";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = (DateTime)reader["MaxDate"];
                            string stringDate = date.ToString("yyyy-MM-dd HH:mm:ss", provider);

                            spotprice.getSpotPrice(stringDate);
                        }
                    }
                    connection.Close();

                }
            }
           



            //truncate all tables
            var exe = SqlStoredProcHelper.ExecuteProc(@"Data Source =.\SQLEXPRESS; Initial Catalog = FuckingNice; Integrated Security = True", "exec dbo.truncateall");

            //Get token access
            string token = await ElOverblikToken.GetToken();

            //general api settings
            string body = @"{
                         ""meteringPoints"": {
                                     ""meteringPoint"": [
                                               ""571313115400203473""
                                                         ]
                                            }
                             }";

            string contentType = "application/json";

            //Get prices
            string priceurl = "https://api.eloverblik.dk/CustomerApi/api/MeteringPoints/MeteringPoint/GetCharges";
            HttpClient clientprice = new HttpClient();

            clientprice.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            clientprice.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resultprice = await clientprice.PostAsync(priceurl, new StringContent(body, Encoding.UTF8, "application/json"));
            string resultContentPrice = await resultprice.Content.ReadAsStringAsync();
            var pricedata = resultContentPrice;

            File.WriteAllText(@"D:\testdata\jsonprices.json", resultContentPrice);
            var datasetPrices = ReadDataFromJsonHelper.ReadDataFromJson(pricedata);

            InsertData.InsertDataSQL(datasetPrices, "insertprices");


            //Get Metering data
            string meteringurl = "https://api.eloverblik.dk/CustomerApi/api/MeterData/GetTimeSeries";
            string fromdate = "2018-01-01";
            string todate = DateTime.Now.ToString("yyyy-MM-dd");
            string Aggregation = "Hour";
            string api_url = meteringurl + "/" + fromdate + "/" + todate + "/" + Aggregation;

            HttpClient clientMetering = new HttpClient();

            clientMetering.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            clientMetering.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resultMetering = await clientMetering.PostAsync(api_url, new StringContent(body, Encoding.UTF8, "application/json"));
            string resultContentMetering = await resultMetering.Content.ReadAsStringAsync();
            var dataMetering = resultContentMetering;

            File.WriteAllText(@"D:\testdata\jsonMetering.json", resultContentMetering);
            var datasetMetering = ReadDataFromJsonHelper.ReadDataFromJson(dataMetering);

            InsertData.InsertDataSQL(datasetMetering, "insertmetering");

            //merge all tables
            var exeMerge = SqlStoredProcHelper.ExecuteProc(@"Data Source =.\SQLEXPRESS; Initial Catalog = FuckingNice; Integrated Security = True", "exec dbo.mergeall");

        }

       


    }
}



