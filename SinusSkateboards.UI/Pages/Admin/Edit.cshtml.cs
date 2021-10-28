using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.UI.Data;
using SinusSkateboards.UI.Models;

namespace SinusSkateboards.UI.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public IFormFile Photo { get; set; }
        public EditModel(SinusSkateboards.UI.Data.WebShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        { 

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Photo != null)
            {
                
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "images");


                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }


                string file = Path.Combine(folder, ProduktModel.ImagePath);

                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }



                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ProduktModel.ProduktId, ".jpg");

                string uploadFolder = Path.Combine(folder, uniqueFileName);

                using (var filestream = new FileStream(uploadFolder, FileMode.Create))
                {
                    Photo.CopyTo(filestream);
                }

                ProduktModel.ImagePath = uniqueFileName;
            }


            _context.Attach(ProduktModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktModelExists(ProduktModel.ProduktId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProduktModelExists(int id)
        {
            return _context.Produkter.Any(e => e.ProduktId == id);
        }
    }
}
