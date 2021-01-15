using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SemestralkaFinalVAII.Models {

    public class ZoznamOblubenych {
        //[ForeignKey("UserId")]
        [Key]
        public string UserId { get; set; }

        public List<OblubenaMena> Oblubene { get; set; }
    }
}
