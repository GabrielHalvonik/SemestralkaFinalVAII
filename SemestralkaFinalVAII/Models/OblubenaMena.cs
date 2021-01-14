using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SemestralkaFinalVAII.Models {

    public class OblubenaMena {
        [ForeignKey("id")]
        public string Id { get; set; }
    }
}
