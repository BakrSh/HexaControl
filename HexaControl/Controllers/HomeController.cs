using HexaControl.Infustructur;
using HexaControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HexaConDbContext _context;


        public HomeController(ILogger<HomeController> logger, HexaConDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {



            var data = new ViewModel.HexaPageData
            {
                Heros = await _context.Heroes.ToListAsync(),
                Services = await _context.Services.ToListAsync(),

                WhyHexas = await _context.WhyHexas.ToListAsync(),
                WhyHexaElements = await _context.WhyHexaElements.ToListAsync(),
                HowWorks = await _context.HowWorks.ToListAsync(),
                HowWeWorks = await _context.howWeWorks.ToListAsync(),
                Blogs = await _context.Blogs.ToListAsync(),
                Banars = await _context.Banars.ToListAsync(),
                Commons = await _context.Commons.ToListAsync(),
                Contacts = await _context.Contacts.ToListAsync(),
                Footers = await _context.Footers.ToListAsync(),
                Socials = await _context.Socials.ToListAsync(),
                Maps = await _context.Maps.ToListAsync()
                
            };
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
