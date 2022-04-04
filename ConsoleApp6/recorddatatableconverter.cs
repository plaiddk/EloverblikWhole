using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp6
{
  public  class recorddatatableconverter
    {

        public class RecordDataTableConverter : Newtonsoft.Json.Converters.DataTableConverter
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                    return null;
                if (reader.TokenType == JsonToken.StartObject)
                {
                    var token = JToken.Load(reader);
                    token = new JArray(token.SelectTokens("records[*]"));
                    using (var subReader = token.CreateReader())
                    {
                        while (subReader.TokenType == JsonToken.None)
                            subReader.Read();
                        return base.ReadJson(subReader, objectType, existingValue, serializer); // Use base class to convert
                    }
                }
                else
                {
                    return base.ReadJson(reader, objectType, existingValue, serializer);
                }
            }
        }
    }
}
