using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HexaControl.Infustructur;
using HexaControl.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WhyHexasController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;

        public WhyHexasController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/WhyHexas
        public async Task<IActionResult> Index()
        {
            return View(await _context.WhyHexas.ToListAsync());
        }



        [HttpPost]
        public  async Task<IActionResult> SaveHexa([FromForm] WhyHexa whyHexa)
        {
            try
            {

                var oldWhyHexa = await _context.WhyHexas.FirstOrDefaultAsync(b => b.Id == whyHexa.Id);

                if (whyHexa.SecIconFile != null)
                {
                    // Get file extension
                    string type = System.IO.Path.GetExtension(whyHexa.SecIconFile.FileName);
                    string name = whyHexa.SecIconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;

                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/WhyHexaFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);

                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await whyHexa.SecIconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }


                    // Delete the existing file if it exists
                    if (oldWhyHexa.SecIconName != null)
                    {
                        var existingFilePath = Path.Combine(rootPath, oldWhyHexa.SecIconName);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }


                    oldWhyHexa.SecIconName = fileName;
                    oldWhyHexa.OriginalName = name;
                }


                oldWhyHexa.SecTitle = whyHexa.SecTitle;
                oldWhyHexa.SubSecText = whyHexa.SubSecText;


                _context.Update(oldWhyHexa);
                await _context.SaveChangesAsync();





                
                return new JsonResult(new { message = "Hexas updates saved." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred while saving the Hexas updates: " + ex.Message });
            }
        }



        // GET: Admin/WhyHexas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyHexa = await _context.WhyHexas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyHexa == null)
            {
                return NotFound();
            }

            return View(whyHexa);
        }

        // GET: Admin/WhyHexas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/WhyHexas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WhyHexa whyHexa)
        {
            if (ModelState.IsValid)
            {

                if (whyHexa.SecIconFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(whyHexa.SecIconFile.FileName);
                    string name = whyHexa.SecIconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/WhyHexaFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await whyHexa.SecIconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    whyHexa.SecIconName = fileName;
                    whyHexa.OriginalName = name;
                }
                _context.Add(whyHexa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(whyHexa);
        }

        // GET: Admin/WhyHexas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
         
            
            

            var whyHexa = await _context.WhyHexas.FindAsync(id);
            if (whyHexa == null)
            {
                return NotFound();
            }
            return View(whyHexa);
        }

        // POST: Admin/WhyHexas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WhyHexa whyHexa)
        {
            if (id != whyHexa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldWhyHexa = await _context.WhyHexas.FirstOrDefaultAsync(b => b.Id == whyHexa.Id);

                    if (whyHexa.SecIconFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(whyHexa.SecIconFile.FileName);
                        string name = whyHexa.SecIconFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/WhyHexaFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await whyHexa.SecIconFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldWhyHexa.SecIconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldWhyHexa.SecIconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldWhyHexa.SecIconName = fileName;
                        oldWhyHexa.OriginalName = name;
                    }


                    oldWhyHexa.SecTitle = whyHexa.SecTitle;
                    oldWhyHexa.SubSecText = whyHexa.SubSecText;


                    _context.Update(oldWhyHexa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhyHexaExists(whyHexa.Id))
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
            return View(whyHexa);
        }

        // GET: Admin/WhyHexas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyHexa = await _context.WhyHexas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyHexa == null)
            {
                return NotFound();
            }

            return View(whyHexa);
        }

        // POST: Admin/WhyHexas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var whyHexa = await _context.WhyHexas.FirstOrDefaultAsync(b => b.Id == id);

            if (whyHexa == null)
            {
                return NotFound();
            }

            var rootPath = Path.Combine(_env.WebRootPath, "AllFiles/WhyHexaFiles");
            var filePath = Path.Combine(rootPath, whyHexa.SecIconName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.WhyHexas.Remove(whyHexa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool WhyHexaExists(int id)
        {
            return _context.WhyHexas.Any(e => e.Id == id);
        }


        //[HttpPost]
        //public IActionResult Save([FromBody] WhyHexa whyHexa, [FromBody] WhyHexaElement whyHexaElement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (whyHexa.Id !=null)
        //    {
               

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                var oldWhyHexa =  _context.WhyHexas.FirstOrDefault(b => b.Id == whyHexa.Id);

        //                if (whyHexa.SecIconFile != null)
        //                {
        //                    // Get file extension
        //                    string type = System.IO.Path.GetExtension(whyHexa.SecIconFile.FileName);
        //                    string name = whyHexa.SecIconFile.FileName;
        //                    string fileName = Guid.NewGuid().ToString() + type;

        //                    string rootPath = Path.Combine(_env.WebRootPath, "WhyHexaFiles");

        //                    // If directory does not exist, create one
        //                    if (!Directory.Exists(rootPath))
        //                        Directory.CreateDirectory(rootPath);

        //                    string filePath = Path.Combine(rootPath, fileName);

        //                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        whyHexa.SecIconFile.CopyTo(FS);
        //                        //Close the File Stream
        //                        FS.Close();
        //                    }


        //                    // Delete the existing file if it exists
        //                    if (oldWhyHexa.SecIconName != null)
        //                    {
        //                        var existingFilePath = Path.Combine(rootPath, oldWhyHexa.SecIconName);
        //                        if (System.IO.File.Exists(existingFilePath))
        //                        {
        //                            System.IO.File.Delete(existingFilePath);
        //                        }
        //                    }


        //                    oldWhyHexa.SecIconName = fileName;
        //                    oldWhyHexa.OriginalName = name;
        //                }


        //                oldWhyHexa.SecTitle = whyHexa.SecTitle;
        //                oldWhyHexa.SubSecText = whyHexa.SubSecText;


        //                _context.Update(oldWhyHexa);
        //                _context.SaveChanges();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!WhyHexaExists(whyHexa.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(whyHexa);
        //    }
        //    // Perform some logic here

        //    return Ok();
        //}

        




    }
    }
