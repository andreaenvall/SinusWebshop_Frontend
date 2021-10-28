using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SinusSkateboards.UI.Models
{
    public class KundModel
    {
        [Key]
        public int Id { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
        public string Email { get; set; }
        public string Stad { get; set; }
        public string Leveransadress { get; set; }
        public long Telefonnummer { get; set; }
        public string Postnummer { get; set; }
        public IList<KöptProdukt> KöptaProdukter { get; set; }
        public DateTime Datum { get; set; }
        public string OrderNummer { get; set; }
       


    }
}
