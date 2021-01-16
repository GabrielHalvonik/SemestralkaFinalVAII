using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemestralkaVAII.Models;

namespace SemestralkaFinalVAII.Models {

    public class ZoznamOblubenych {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("Id")]
        public List<string> Oblubene { get; set; }
    }
}
