using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SinusSkateboards.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.UI.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
        public static List<KöptProdukt> CookieLista { get; set; } = new List<KöptProdukt>();

        public void OnGet()
        {
            var cartString = HttpContext.Session.GetString("Produkter");

            List<KöptProdukt> cart = new List<KöptProdukt>();

            if (!String.IsNullOrEmpty(cartString))
            {

                cart = JsonConvert.DeserializeObject<List<KöptProdukt>>(cartString);

            }
            foreach (var item in cart)
            {
                CookieLista.Add(item);
            }


            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Produkter", cartString);
        }
    }
}
