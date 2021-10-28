using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SinusSkateboards.UI.Data;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages
{
    public class ProductListModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
       
        [BindProperty]
        public int? Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public ProduktKategori? Kategori { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Färg { get; set; }
        public ProductListModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }


        public IList<ProduktModel> ProduktModel { get;set; }

        public async Task OnGetAsync()
        {
           
            var products = from m in _context.Produkter
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.ProduktNamn.Contains(SearchString));
            }

            if (Kategori != null)
            {
                products = products.Where(s => s.Produktkategori == Kategori);

            }

            if(Färg != null)
            {
                products = products.Where(s => s.Färg == Färg);
            }

            ProduktModel = await products.ToListAsync();

           

        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
           
            
            var produkt = await _context.Produkter.FirstOrDefaultAsync(m => m.ProduktId == id);

            var kp = new KöptProdukt
            {
                Id = produkt.ProduktId,
                Färg = produkt.Färg,
                ImgPath = produkt.ImagePath,
                ProduktNamn = produkt.ProduktNamn,
                Produktnummer = produkt.Produktnummer,
                Pris = produkt.Pris,
                Produktkategori = produkt.Produktkategori,
                Antal = 1,
                Beskrivning = produkt.Beskrivning
                
            };

          

            var cartString = HttpContext.Session.GetString("Produkter");
           

            List<KöptProdukt> cart = new List<KöptProdukt>();

            if (!String.IsNullOrEmpty(cartString))
            {
                cart = JsonConvert.DeserializeObject<List<KöptProdukt>>(cartString);
               
            }
            var prodID = 0;

            foreach(var item in cart)
            {
                if(kp.ProduktNamn == item.ProduktNamn)
                {
                    item.Antal++;
                    item.Pris += kp.Pris;
                    prodID = item.Id;


                }
            }
            if(prodID != kp.Id)
            {
                cart.Add(kp);
            }
            


            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Produkter", cartString);


           

            return RedirectToPage("/ProductList");
        }

       
    }
}
