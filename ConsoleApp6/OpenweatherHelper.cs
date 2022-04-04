using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    class OpenweatherHelper
    {
        //Getopenweather data


        //string openweatherurl = "https://api.openweathermap.org/data/2.5/onecall?lat=56.453&lon=9.402&exclude=minutely,daily&appid=bc6ae807fd69847b7296e9bc609af312&units=metric&lang=da";
        //HttpClient clientOpenweather = new HttpClient();
        //HttpResponseMessage resOpen = await clientOpenweather.GetAsync(openweatherurl);
        //resOpen.EnsureSuccessStatusCode();
        //var bodyContentOpenWeather = await resOpen.Content.ReadAsStringAsync();

        //File.WriteAllText(@"D:\testdata\jsonopenweather.json", bodyContentOpenWeather);


        ////// Note:Json convertor needs a json with one node as root 
        //string jsonString = "{ \"rootNode\": {" + bodyContentOpenWeather.Trim().TrimStart('{').TrimEnd('}') + @"} }";
        ////// Now it is secure that we have always a Json with one node as root  
        //var xd = JsonConvert.DeserializeXmlNode(jsonString);
        ////// DataSet is able to read from XML and return a proper DataSet 
        //DataSet result = new DataSet();
        //result.ReadXml(new XmlNodeReader(xd));

    }
}
