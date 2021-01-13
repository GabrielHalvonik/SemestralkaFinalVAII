using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaVAII.Models {

    [Serializable]
    public class HistoriaCeny {

        [JsonPropertyName("prices")]
        public double[][] Prices { get; set; }

        [JsonPropertyName("market_caps")]
        public double[][] MarketCaps { get; set; }

        [JsonPropertyName("total_volumes")]
        public double[][] TotalVolumes { get; set; }
    }
}