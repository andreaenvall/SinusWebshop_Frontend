using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages.Admin
{
    public class OrderDetailsModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
        
        public OrderDetailsModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public KundModel KundModel { get; set; }
        public async Task OnGet(string ordernummer, int id)
        {
            KundModel = await _context.KundModel.FirstOrDefaultAsync(m => m.OrderNummer == ordernummer);
            KundModel = await _context.KundModel.FirstOrDefaultAsync(m => m.Id == id);

            List<KöptProdukt> newList = _context.KöptProdukt.Where(m => m.KundId == id)
                .Select(m => new KöptProdukt
                        {
                             
                             ProduktNamn = m.ProduktNamn,
                             Beskrivning = m.Beskrivning,
                             Färg = m.Färg,
                             Produktkategori = m.Produktkategori,
                             Produktnummer = m.Produktnummer,
                             Antal = m.Antal
                         }).ToList();

            KundModel.KöptaProdukter = newList;
            
        }
    }
}
