using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Models {

    public class OblubenaMena {
        //[InverseProperty("Oblubene")]
        //[ForeignKey("id")]
        public string Id { get; set; }

        Kryptomeny zoznam;
    }
}
