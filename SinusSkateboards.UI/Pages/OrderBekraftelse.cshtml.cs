using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.UI.Models;
using SinusSkateboards.UI.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace SinusSkateboards.UI.Pages
{
    public class OrderBekraftelseModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
        [BindProperty]
        public KundModel KundModel { get; set; } = new KundModel();

        [BindProperty]
        public double SummaPris { get; set; }

        public double Rabatt { get; set; }
        public OrderBekraftelseModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            SummaPris = 0;
           
        }
        public void OnGetSingleOrder(int KöpId, double Rabatt)
        {
            this.Rabatt = Rabatt;

            var order = from m in _context.KundModel
                        select m;

            order = order.Where(s => s.Id == KöpId);

            KundModel = order.FirstOrDefault();

            List<KöptProdukt> newList = _context.KöptProdukt.Where(m => m.KundId == KöpId)

               .Select(m => new KöptProdukt
               {
                   KundId = m.KundId,
                   ProduktNamn = m.ProduktNamn,
                   Beskrivning = m.Beskrivning,
                   Färg = m.Färg,
                   Produktkategori = m.Produktkategori,
                   Produktnummer = m.Produktnummer,
                   ImgPath = m.ImgPath,
                   Pris = m.Pris,
                   Antal = m.Antal


               }).ToList();

            KundModel.KöptaProdukter = newList;

            List<KöptProdukt> produkter = new List<KöptProdukt>();

            string stringprodukter = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukter))
            {
                produkter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukter);

            }

            produkter.Clear();

            stringprodukter = JsonConvert.SerializeObject(produkter);
            HttpContext.Session.SetString("Produkter", stringprodukter);



        }
    }
}
