using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp6
{
    class DMI
    {
        public static void insertDMI(DataTable dt, string data)
        {

            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=FuckingNice; Integrated Security=True"))
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(conn))
                {
                    if (data.Equals("DMITemp"))
                    {
                        conn.Open();
                        bulk.DestinationTableName = "DMITemp";
                        bulk.WriteToServer(dt);
                        conn.Close();
                    }
                    else
                    {

                        conn.Open();
                        bulk.DestinationTableName = "DMIweather";
                        bulk.WriteToServer(dt);
                        conn.Close();
                    }
                }
            }

        }

    }
}
//Update this to v2 https://confluence.govcloud.dk/pages/viewpage.action?pageId=26476799

////Get dmi data temperature
//string dmiurl = "https://dmigw.govcloud.dk/metObs/v1/observation?stationId=06068&parameterId=temp_mean_past1h&api-key=a4d234fb-b3cf-4ef4-bd0a-c7257234487d&limit=25000";
////string dmiurl = "https://api.openweathermap.org/data/2.5/onecall?lat=56.453&lon=9.402&exclude=minutely,daily&appid=bc6ae807fd69847b7296e9bc609af312&units=metric&lang=da";
//HttpClient clientDmi = new HttpClient();
//HttpResponseMessage resDmi = await clientDmi.GetAsync(dmiurl);
//resDmi.EnsureSuccessStatusCode();
//var bodycontentDmi = await resDmi.Content.ReadAsStringAsync();

//File.WriteAllText(@"D:\testdata\jsontempdmi.json", bodycontentDmi);

//DataTable dt = jsonToDataTable.toDataTable(bodycontentDmi);

//DMI.insertDMI(dt, "DMITemp");



//////Get weather
//string weatherurl = "https://dmigw.govcloud.dk/metObs/v1/observation?stationId=06041&parameterId=weather&api-key=a4d234fb-b3cf-4ef4-bd0a-c7257234487d&limit=50000";

//HttpClient clientWeather = new HttpClient();
//HttpResponseMessage resweather = await clientWeather.GetAsync(weatherurl);
//resweather.EnsureSuccessStatusCode();
//var bodycontentWeather = await resweather.Content.ReadAsStringAsync();


//DataTable dtWeather = jsonToDataTable.toDataTable(bodycontentWeather);

//DMI.insertDMI(dtWeather, "DMIweather");