using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.UI.Models
{
    public enum ProduktKategori
    {
        Skateboards, Hoodies, Tshirts, Keps, Skateboardtillbehör
    }
   
    public class ProduktModel
    {
        [Key]
        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }
        public ProduktKategori Produktkategori { get; set; }
        public string Produktnummer { get; set; }
        public string Färg { get; set; }
        public double Pris { get; set; }
        
        public string ImagePath { get; set; }
        public string Beskrivning { get; set; }
       
    }
}
