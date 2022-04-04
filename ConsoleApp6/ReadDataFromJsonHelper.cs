using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace ConsoleApp6
{
    class ReadDataFromJsonHelper
    {
        public static DataSet ReadDataFromJson(string jsonString, XmlReadMode mode = XmlReadMode.Auto)
        {
            //// Note:Json convertor needs a json with one node as root
            //jsonString = "jsonString.Trim().TrimStart('{').TrimEnd('}') + @";
            //// Now it is secure that we have always a Json with one node as root 
            var xd = JsonConvert.DeserializeXmlNode(jsonString);
            //// DataSet is able to read from XML and return a proper DataSet
            var result = new DataSet();
            result.ReadXml(new XmlNodeReader(xd), mode);
            return result;
        }
    }
}
