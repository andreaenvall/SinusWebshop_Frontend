using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.UI.Data
{
    public class WebShopDbContext : IdentityDbContext<IdentityUser>
    {
        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
        {

        }

        //public DbSet<OrderHistorikModel> Bestallningar { get; set; }
        //public DbSet<OrderedProducts> OrderedProducts { get; set; }
        public DbSet<ProduktModel> Produkter { get; set; }
        public DbSet<KöptProdukt> KöptProdukt { get; set; }
        public DbSet<SinusSkateboards.UI.Models.KundModel> KundModel { get; set; }

    }
}
