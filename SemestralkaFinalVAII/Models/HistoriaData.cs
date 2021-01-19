using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SemestralkaFinalVAII.Models {

    [Keyless]
    public class HistoriaData {

        [ForeignKey("Id")]
        public string IdMeny { get; set; }

        public List<HistoriaDataZaznam> Prices { get; set; }

        public List<HistoriaDataZaznam> MarketCaps { get; set; }

        public List<HistoriaDataZaznam> TotalVolumes { get; set; }
    }
}
