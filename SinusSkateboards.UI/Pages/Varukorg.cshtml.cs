using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages
{
    [BindProperties]
    public class VarukorgModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
        public int SumVaror { get; set; }
        public double SumPrize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Antal { get; set; }

        public int id { get; set; }

        [BindProperty]
        public double Rabatt { get; set; }

        [BindProperty]
        public double RabattPris { get; set; }

        public List<KöptProdukt> CookieProdukter { get; set; } = new List<KöptProdukt>();

        public VarukorgModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }



        

        public async Task OnGetAsync(int ID, double Rabatt, double RabattPris)
        {
            SumPrize = 0;
            SumVaror = 0;
            this.Rabatt = Math.Round(Rabatt, 1, MidpointRounding.ToEven);
            this.RabattPris = Math.Round(RabattPris, 1, MidpointRounding.ToEven);
            

            
            var index = CookieProdukter.FindIndex(c => c.Id == ID);

            if (index >= 0 || Antal != 0)
            {
                CookieProdukter[index].Antal = Antal;
            }

            string stringprodukt = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukt))
            {
                CookieProdukter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukt);
            }

        }
        public void OnPost(int id)
        {


            List<KöptProdukt> produkter = new List<KöptProdukt>();

            string stringprodukter = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukter))
            {
                produkter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukter);

            }

            

            stringprodukter = JsonConvert.SerializeObject(produkter);
            HttpContext.Session.SetString("Produkter", stringprodukter);




            RedirectToPage("./Varukorg");
        }
        public IActionResult OnPostPlus(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<KöptProdukt> produkter = new List<KöptProdukt>();

            string stringprodukter = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukter))
            {
                produkter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukter);

            }

            var product = _context.Produkter.FirstOrDefault(m => m.ProduktId == id);

            foreach (var prod in produkter)
            {
                if (prod.Id == id)
                {
                    prod.Antal++;
                    prod.Pris += product.Pris;
                }
            }
            stringprodukter = JsonConvert.SerializeObject(produkter);
            HttpContext.Session.SetString("Produkter", stringprodukter);


            return RedirectToPage("/Varukorg");
        }

        public IActionResult OnPostMinus(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<KöptProdukt> produkter = new List<KöptProdukt>();

            string stringprodukter = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukter))
            {
                produkter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukter);

            }

            var product = _context.Produkter.FirstOrDefault(m => m.ProduktId == id);

            foreach (var prod in produkter)
            {
                if (prod.Id == id)
                {
                    prod.Antal--;
                    prod.Pris -= product.Pris;
                    
                }
            }

            stringprodukter = JsonConvert.SerializeObject(produkter);
            HttpContext.Session.SetString("Produkter", stringprodukter);


            return RedirectToPage("/Varukorg");
        }
        public IActionResult OnPostDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<KöptProdukt> produkter = new List<KöptProdukt>();

            string stringprodukter = HttpContext.Session.GetString("Produkter");

            if (!string.IsNullOrEmpty(stringprodukter))
            {
                produkter = JsonConvert.DeserializeObject<List<KöptProdukt>>(stringprodukter);

            }

            var product = _context.Produkter.FirstOrDefault(m => m.ProduktId == id);

            foreach (var prod in produkter)
            {
                if (prod.Id == id)
                {
                    produkter.Remove(prod);
                    break;

                }
            }

            stringprodukter = JsonConvert.SerializeObject(produkter);
            HttpContext.Session.SetString("Produkter", stringprodukter);


            return RedirectToPage("/Varukorg");
        }

        public IActionResult OnPostRabatt(string rabatt, int Sumpris)
        {
            if (rabatt.Equals("andrea20"))
            {

                Rabatt = Sumpris * 0.2;
                RabattPris = Sumpris - Rabatt;
            }

            return RedirectToPage("/Varukorg", new { Rabatt, RabattPris});
        }

    }
}
