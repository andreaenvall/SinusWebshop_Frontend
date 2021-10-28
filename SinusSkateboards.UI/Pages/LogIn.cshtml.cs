using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SinusSkateboards.UI.Pages
{
    [BindProperties]
    public class LogInModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LogInModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Username, Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
                else
                {
                    ErrorMessage = "Username or password was incorrect, Please try again";
                }


            }
           
            return Page();
        }
    }
}
