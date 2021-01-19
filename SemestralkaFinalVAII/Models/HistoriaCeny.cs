using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaVAII.Models {

    [Serializable]
    public class HistoriaCeny {

        //[ForeignKey("Id")]
        //public string IdMeny { get; set; }

        [JsonPropertyName("prices")]
        public double[][] Prices { get; set; }

        [JsonPropertyName("market_caps")]
        public double?[][] MarketCaps { get; set; }

        [JsonPropertyName("total_volumes")]
        public double?[][] TotalVolumes { get; set; }
    }
}