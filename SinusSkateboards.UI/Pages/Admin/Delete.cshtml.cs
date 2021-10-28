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
    public class DeleteModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;

        public DeleteModel(SinusSkateboards.UI.Data.WebShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProduktModel ProduktModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProduktModel = await _context.Produkter.FirstOrDefaultAsync(m => m.ProduktId == id);

            if (ProduktModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProduktModel = await _context.Produkter.FindAsync(id);

            if (ProduktModel != null)
            {
                _context.Produkter.Remove(ProduktModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
