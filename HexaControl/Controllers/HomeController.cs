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


        public async Task<IActionResult> Blogs()
        {
            return View(await _context.Blogs.ToListAsync());
        }




        // GET: Home/blogs/Details/5
        public async Task<IActionResult> BlogDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = new ViewModel.BlogDetailsVM
            {
                Blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id),
                LstBlog = await _context.Blogs.ToListAsync(),
        };
            




            if (data.Blog == null)
            {
                return NotFound();
            }

            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitForm(Contact contact)
        {
          
            _context.Add(contact);
            await _context.SaveChangesAsync();

            // Return a JSON response indicating success or failure
            return Json(new { success = true });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
