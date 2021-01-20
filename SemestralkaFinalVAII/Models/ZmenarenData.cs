using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SemestralkaVAII.Models {

    public class ZmenarenData {

        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("year_established")]
        public long? YearEstablished { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("image")]
        public Uri Image { get; set; }

        [JsonPropertyName("trust_score")]
        public long TrustScore { get; set; }

        [JsonPropertyName("trust_score_rank")]
        public long TrustScoreRank { get; set; }

        [JsonPropertyName("trade_volume_24h_btc")]
        public double TradeVolume24HBtc { get; set; }

        [JsonPropertyName("trade_volume_24h_btc_normalized")]
        public double TradeVolume24HBtcNormalized { get; set; }
    }
}
