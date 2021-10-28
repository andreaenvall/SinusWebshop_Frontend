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
    public class OrderListModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;

        //public KundModel KundModel { get; set; }
        public OrderListModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }
        public IList<KundModel> KundModel { get; set; }

        public async Task OnGetAsync()
        {
            KundModel = await _context.KundModel.ToListAsync();
        }
    }
}
