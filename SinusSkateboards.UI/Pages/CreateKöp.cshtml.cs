using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SinusSkateboards.UI.Data;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages
{
    public class CreateKöpModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
        public List<KöptProdukt> KöptaProdukter { get; set; } = new List<KöptProdukt>();

       
        public double Rabatt { get; set; }

        public CreateKöpModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(double rabatt)
        {
            Rabatt = rabatt;
           
            return Page();
        }

        [BindProperty]
        public KundModel KundModel { get; set; }
        
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(double rabatt)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var cartString = HttpContext.Session.GetString("Produkter");


            List<KöptProdukt> cart = new List<KöptProdukt>();

            if (!String.IsNullOrEmpty(cartString))
            {
                cart = JsonConvert.DeserializeObject<List<KöptProdukt>>(cartString);

            }

            

            foreach (var item in cart)
            {
                var KöptProdukt = new KöptProdukt();

                KöptProdukt.Beskrivning = item.Beskrivning;
                KöptProdukt.Färg = item.Färg;
                KöptProdukt.Pris = item.Pris;
                KöptProdukt.Produktkategori = item.Produktkategori;
                KöptProdukt.ProduktNamn = item.ProduktNamn;
                KöptProdukt.Produktnummer = item.Produktnummer;
               
                KöptProdukt.Kund = KundModel;
                KöptProdukt.KundId = KundModel.Id;
                KöptProdukt.ImgPath = item.ImgPath;
                KöptProdukt.Antal = item.Antal;

                KöptaProdukter.Add(KöptProdukt);

            }

            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Produkter", cartString);

            KundModel.Datum = DateTime.Now;

            int g = Math.Abs(Guid.NewGuid().GetHashCode());
            
            string ordernum = "";

            foreach(var num in g.ToString())
            {
                
                if(ordernum.Length >= 11)
                {
                    break;
                }
                else
                {
                    
                    ordernum += num;
                }
            }
            
            KundModel.OrderNummer = ordernum;

            KundModel.KöptaProdukter = KöptaProdukter;


            _context.KundModel.Add(KundModel);


            await _context.SaveChangesAsync();

            KöptaProdukter.Clear();

            return RedirectToPage("OrderBekraftelse", "SingleOrder", new { KöpId = KundModel.Id, Rabatt = rabatt });
        }
    }
}
