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

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;

        public ServicesController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        // GET: Admin/Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Admin/Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Service service)
        {
            if (ModelState.IsValid)
            {
                if (service.IconFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(service.IconFile.FileName);
                    string name = service.IconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/ServcesFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await service.IconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    service.IconName = fileName;
                    service.OriginalName = name;
                }
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Admin/Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Admin/Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldService = await _context.Services.FirstOrDefaultAsync(b => b.Id == service.Id);

                    if (service.IconFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(service.IconFile.FileName);
                        string name = service.IconFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/ServcesFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await service.IconFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldService.IconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldService.IconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldService.IconName = fileName;
                        oldService.OriginalName = name;
                    }


                    oldService.IconText = service.IconText;
                   
                    _context.Update(oldService);

                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Admin/Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(b => b.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            // Delete the file if it exists
            if (service.IconName != null)
            {
                var rootPath = Path.Combine(_env.WebRootPath, "AllFiles/ServcesFiles");
                var filePath = Path.Combine(rootPath, service.IconName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
           

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
