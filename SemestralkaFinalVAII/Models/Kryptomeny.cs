using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SemestralkaFinalVAII.Models;

namespace SemestralkaVAII.Models {

    [Serializable]
    public class Kryptomeny {
        [JsonPropertyName("id")]
        [Key]
        public string Id { get; set; }

        [JsonPropertyName("image")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("name")]
        [Required]
        [StringLength(30)]
        public string Nazov { get; set; }

        [JsonPropertyName("current_price")]
        [Required]
        public double Cena { get; set; }

        [JsonPropertyName("price_change_24h")]
        [Required]
        public double Zmena { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        [Required]
        public double ZmenaPercent { get; set; }

        [JsonPropertyName("total_volume")]
        public long PocetTokenov { get; set; }

        [JsonPropertyName("market_cap")]
        public long CenaCelkovo { get; set; }
    }
}