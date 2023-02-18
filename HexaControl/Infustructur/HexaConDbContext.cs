using HexaControl.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Infustructur
{
    public class HexaConDbContext : IdentityDbContext

    {
        public HexaConDbContext(DbContextOptions<HexaConDbContext> options) : base(options)
        {

        }
        public DbSet<Hero>  Heroes { get; set; }
        public DbSet<Banar>  Banars { get; set; }
        public DbSet<Blog>  Blogs { get; set; }
        public DbSet<CommonQues>  Commons { get; set; }
        public DbSet<Contact>  Contacts { get; set; }
        public DbSet<Footer>  Footers { get; set; }
        public DbSet<HowWork>  HowWorks { get; set; }
        public DbSet<HowWeWorkElement>  howWeWorks { get; set; }
        public DbSet<Map>  Maps { get; set; }
        public DbSet<Service>  Services { get; set; }
        public DbSet<Social>  Socials { get; set; }
        public DbSet<WhyHexa>  WhyHexas { get; set; }
        public DbSet<WhyHexaElement>  WhyHexaElements { get; set; }
    }
    
}
