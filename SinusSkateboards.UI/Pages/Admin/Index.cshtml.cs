using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.UI.Data;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;

        public IndexModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }

        public IList<ProduktModel> ProduktModel { get;set; }

        public async Task OnGetAsync()
        {
            ProduktModel = await _context.Produkter.ToListAsync();
        }
    }
}
