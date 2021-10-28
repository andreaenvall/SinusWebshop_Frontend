using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.UI.Models
{
    public enum Storlek
    {
        XS, S, M, L, XL, XXL
    }
    public class KöptProdukt
    {

        public int Id { get; set; }
        public string ProduktNamn { get; set; }
        public ProduktKategori Produktkategori { get; set; }
        public string Produktnummer { get; set; }
        public string Färg { get; set; }
        public double Pris { get; set; }
        
        public string Beskrivning { get; set; }
        public KundModel Kund { get; set; }
        public int KundId { get; set; }
        public string ImgPath { get; set; }

        public int Antal { get; set; }



    }
}
