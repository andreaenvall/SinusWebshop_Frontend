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
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SinusSkateboards.UI.Data.WebShopDbContext _context;

        [BindProperty]
        public ProduktModel ProduktModel { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public CreateModel(SinusSkateboards.UI.Data.WebShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

       

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string folder = Path.Combine(webHostEnvironment.WebRootPath, "images");


            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ProduktModel.ProduktId, ".jpg");

            string uploadFolder = Path.Combine(folder, uniqueFileName);

            using (var filestream = new FileStream(uploadFolder, FileMode.Create))
            {
                Photo.CopyTo(filestream);
            }

            ProduktModel.ImagePath = uniqueFileName;
            _context.Produkter.Add(ProduktModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
