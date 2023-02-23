using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HexaControl.Infustructur;
using HexaControl.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class BanarsController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;

        public BanarsController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Banars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banars.ToListAsync());
        }

        // GET: Admin/Banars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banar = await _context.Banars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banar == null)
            {
                return NotFound();
            }

            return View(banar);
        }

        // GET: Admin/Banars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Banar banar)
        {
            if (ModelState.IsValid)
            {
                if (banar.File != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(banar.File.FileName);
                    string name = banar.File.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BanarFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await banar.File.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    banar.FileName = fileName;
                    banar.OriginalName = name;
                }

                _context.Add(banar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banar);
        }

        // GET: Admin/Banars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banar = await _context.Banars.FindAsync(id);
            if (banar == null)
            {
                return NotFound();
            }
            return View(banar);
        }

        // POST: Admin/Banars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Banar banar)
        {
            if (id != banar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldbanar = await _context.Banars.FirstOrDefaultAsync(b => b.Id == banar.Id);

                    if (banar.File != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(banar.File.FileName);
                        string name = banar.File.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BanarFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await banar.File.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldbanar.FileName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldbanar.FileName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldbanar.FileName = fileName;
                        oldbanar.OriginalName = name;
                    }


                    oldbanar.Prgraph = banar.Prgraph;  
                    oldbanar.Url = banar.Url;
                    _context.Update(oldbanar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BanarExists(banar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banar);
        }

        // GET: Admin/Banars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banar = await _context.Banars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banar == null)
            {
                return NotFound();
            }

            return View(banar);
        }

        // POST: Admin/Banars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var banar = await _context.Banars.FindAsync(id);
            if (banar == null)
            {
                return NotFound();
            }

            // Delete the file if it exists
            if (banar.FileName != null)
            {
                string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BanarFiles");
                var existingFilePath = Path.Combine(rootPath, banar.FileName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            _context.Banars.Remove(banar);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool BanarExists(int id)
        {
            return _context.Banars.Any(e => e.Id == id);
        }
    }
}
