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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HowWorksController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;


        public HowWorksController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/HowWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.HowWorks.ToListAsync());
        }



        [HttpPost]
        public async Task<IActionResult> SaveHowWork([FromForm] HowWork howWork)
        {
            try
            {

                var oldHowWork = await _context.HowWorks.FirstOrDefaultAsync(b => b.Id == howWork.Id);

                if (howWork.SecIconFile != null)
                {
                    // Get file extension
                    string type = System.IO.Path.GetExtension(howWork.SecIconFile.FileName);
                    string name = howWork.SecIconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;

                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/HowWorkFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);

                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await howWork.SecIconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }


                    // Delete the existing file if it exists
                    if (oldHowWork.SecIconName != null)
                    {
                        var existingFilePath = Path.Combine(rootPath, oldHowWork.SecIconName);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }


                    oldHowWork.SecIconName = fileName;
                    oldHowWork.OriginalName = name;
                }


                oldHowWork.SecTitle = howWork.SecTitle;



                _context.Update(oldHowWork);
                await _context.SaveChangesAsync();




                return new JsonResult(new { message = "How WeWork updates saved." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred while saving the How WeWork updates: " + ex.Message });
            }
        }


        // GET: Admin/HowWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWork = await _context.HowWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (howWork == null)
            {
                return NotFound();
            }

            return View(howWork);
        }

        // GET: Admin/HowWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HowWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( HowWork howWork)
        {
            if (ModelState.IsValid)
            {
                if (howWork.SecIconFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(howWork.SecIconFile.FileName);
                    string name = howWork.SecIconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/HowWorkFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await howWork.SecIconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    howWork.SecIconName = fileName;
                    howWork.OriginalName = name;
                }

                _context.Add(howWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(howWork);
        }

        // GET: Admin/HowWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWork = await _context.HowWorks.FindAsync(id);
            if (howWork == null)
            {
                return NotFound();
            }
            return View(howWork);
        }

        // POST: Admin/HowWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HowWork howWork)
        {
            if (id != howWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldHowWork = await _context.HowWorks.FirstOrDefaultAsync(b => b.Id == howWork.Id);

                    if (howWork.SecIconFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(howWork.SecIconFile.FileName);
                        string name = howWork.SecIconFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/HowWorkFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await howWork.SecIconFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldHowWork.SecIconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldHowWork.SecIconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldHowWork.SecIconName = fileName;
                        oldHowWork.OriginalName = name;
                    }


                    oldHowWork.SecTitle = howWork.SecTitle;
                   


                    _context.Update(oldHowWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HowWorkExists(howWork.Id))
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
            return View(howWork);
        }

        // GET: Admin/HowWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWork = await _context.HowWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (howWork == null)
            {
                return NotFound();
            }

            return View(howWork);
        }

        // POST: Admin/HowWorks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var howWork = await _context.HowWorks.FindAsync(id);
            if (howWork == null)
            {
                return NotFound();
            }

            // Delete the associated file
            if (howWork.SecIconName != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, "AllFiles/HowWorkFiles", howWork.SecIconName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.HowWorks.Remove(howWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool HowWorkExists(int id)
        {
            return _context.HowWorks.Any(e => e.Id == id);
        }
    }
}
