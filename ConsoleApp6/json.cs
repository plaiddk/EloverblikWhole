using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public partial class JsonTable
    {
        [JsonProperty("result")]
        public Result[] result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("MyEnergyData_MarketDocument")]
        public Myenergydata_Marketdocument MyEnergyData_MarketDocument { get; set; }
        [JsonProperty("success")]
        public bool success { get; set; }
        [JsonProperty("errorCode")]
        public string errorCode { get; set; }
        [JsonProperty("errorText")]
        public string errorText { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("stackTrace")]
        public object stackTrace { get; set; }
    }

    public partial class Myenergydata_Marketdocument
    {
        [JsonProperty("mRID")]
        public string mRID { get; set; }
        [JsonProperty("createdDateTime")]
        public DateTime createdDateTime { get; set; }
        [JsonProperty("sender_MarketParticipant.name")]
        public string sender_MarketParticipantname { get; set; }
        [JsonProperty("sender_MarketParticipant.mRID")]
        public Sender_MarketparticipantMrid sender_MarketParticipantmRID { get; set; }
        [JsonProperty("period.timeInterval")]
        public PeriodTimeinterval periodtimeInterval { get; set; }
        [JsonProperty("TimeSeries")]
        public Timesery[] TimeSeries { get; set; }
    }

    public partial class Sender_MarketparticipantMrid
    {
        [JsonProperty("codingScheme")]
        public object codingScheme { get; set; }
        [JsonProperty("name")]
        public object name { get; set; }
    }

    public partial class PeriodTimeinterval
    {
        [JsonProperty("start")]
        public DateTime start { get; set; }
        [JsonProperty("end")]
        public DateTime end { get; set; }
    }

    public partial class Timesery
    {
        [JsonProperty("mRID")]
        public string mRID { get; set; }
        [JsonProperty("businessType")]
        public string businessType { get; set; }
        [JsonProperty("curveType")]
        public string curveType { get; set; }
        [JsonProperty("measurement_Unit.name")]
        public string measurement_Unitname { get; set; }
        [JsonProperty("MarketEvaluationPoint")]
        public Marketevaluationpoint MarketEvaluationPoint { get; set; }
        [JsonProperty("Period")]
        public Period[] Period { get; set; }
    }

    public partial class Marketevaluationpoint
    {
        [JsonProperty("mRID")]
        public Mrid mRID { get; set; }
    }

    public partial class Mrid
    {
        [JsonProperty("codingScheme")]
        public string codingScheme { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }

    }

    public partial class Period
    {
        [JsonProperty("resolution")]
        public string resolution { get; set; }
        [JsonProperty("timeInterval")]
        public Timeinterval timeInterval { get; set; }
        [JsonProperty("Point")]
        public Point[] Point { get; set; }
    }

    public partial class Timeinterval
    {
        [JsonProperty("start")]
        public DateTime start { get; set; }
        [JsonProperty("end")]
        public DateTime end { get; set; }
    }

    public partial class Point
    {
        [JsonProperty("position")]
        public string position { get; set; }
        [JsonProperty("out_Quantity.quantity")]
        public string out_Quantityquantity { get; set; }
        [JsonProperty("out_Quantity.quality")]
        public string out_Quantityquality { get; set; }
    }


}
