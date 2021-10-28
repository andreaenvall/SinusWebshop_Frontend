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
    public class DetailsProduktModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;

     
        public DetailsProduktModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }

        public List<ProduktModel> Produkter { get; set; } = new List<ProduktModel>();
        public ProduktModel ProduktModel { get; set; }
       

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produkter = await _context.Produkter.Where(x => x.ProduktId > 0).ToListAsync();

            ProduktModel = await _context.Produkter.FirstOrDefaultAsync(m => m.ProduktId == id);

            if (ProduktModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int? id, int Antal)
        {
            ProduktModel = await _context.Produkter.FirstOrDefaultAsync(m => m.ProduktId == id);

            if (ProduktModel == null)
            {
                return NotFound();
            }

            var kp = new KöptProdukt
            {
                Id = ProduktModel.ProduktId,
                Färg = ProduktModel.Färg,
                ImgPath = ProduktModel.ImagePath,
                ProduktNamn = ProduktModel.ProduktNamn,
                Produktnummer = ProduktModel.Produktnummer,
                Pris = ProduktModel.Pris * Antal,
                Produktkategori = ProduktModel.Produktkategori,
                Antal = Antal,
                Beskrivning = ProduktModel.Beskrivning

            };


            var cartString = HttpContext.Session.GetString("Produkter");
           
            List<KöptProdukt> cart = new List<KöptProdukt>();

            if (!String.IsNullOrEmpty(cartString))
            {
                cart = JsonConvert.DeserializeObject<List<KöptProdukt>>(cartString);

            }
            var prodID = 0;

            foreach (var item in cart)
            {
                if (kp.ProduktNamn == item.ProduktNamn)
                {
                    item.Antal += kp.Antal;
                    item.Pris += kp.Pris;
                    prodID = item.Id;


                }
            }
            if (prodID != kp.Id)
            {
                cart.Add(kp);
            }
            

            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Produkter", cartString);


           // OrderModel.AddProdukt(ProduktModel);

            return RedirectToPage("./ProductList");
        }
    }
}
