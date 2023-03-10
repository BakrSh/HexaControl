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
    public class WhyHexaElementsController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;

        public WhyHexaElementsController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/WhyHexaElements
        public async Task<IActionResult> Index()
        {
            var hexaConDbContext = _context.WhyHexaElements.Include(w => w.whyHexa);
            return View(await hexaConDbContext.ToListAsync());
        }

        // GET: Admin/WhyHexaElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyHexaElement = await _context.WhyHexaElements
                .Include(w => w.whyHexa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyHexaElement == null)
            {
                return NotFound();
            }

            return View(whyHexaElement);
        }

        // GET: Admin/WhyHexaElements/Create
        public IActionResult Create()
        {
            ViewData["WhyHexaId"] = new SelectList(_context.WhyHexas, "Id", "Id");
            return View();
        }

        // POST: Admin/WhyHexaElements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WhyHexaElement whyHexaElement)
        {
            if (ModelState.IsValid)
            {
                if (whyHexaElement.IconFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(whyHexaElement.IconFile.FileName);
                    string name = whyHexaElement.IconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/whyHexaElementFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await whyHexaElement.IconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    whyHexaElement.IconName = fileName;
                    whyHexaElement.OriginalName = name;
                }
                _context.Add(whyHexaElement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WhyHexaId"] = new SelectList(_context.WhyHexas, "Id", "Id", whyHexaElement.WhyHexaId);
            return View(whyHexaElement);
        }

        // GET: Admin/WhyHexaElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyHexaElement = await _context.WhyHexaElements.FindAsync(id);
            if (whyHexaElement == null)
            {
                return NotFound();
            }
            ViewData["WhyHexaId"] = new SelectList(_context.WhyHexas, "Id", "Id", whyHexaElement.WhyHexaId);
            return View(whyHexaElement);
        }

        // POST: Admin/WhyHexaElements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  WhyHexaElement whyHexaElement)
        {
            if (id != whyHexaElement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldwhyHexaElement = await _context.WhyHexaElements.FirstOrDefaultAsync(b => b.Id == whyHexaElement.Id);

                    if (whyHexaElement.IconFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(whyHexaElement.IconFile.FileName);
                        string name = whyHexaElement.IconFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/whyHexaElementFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await whyHexaElement.IconFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldwhyHexaElement.IconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldwhyHexaElement.IconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldwhyHexaElement.IconName = fileName;
                        oldwhyHexaElement.OriginalName = name;
                    }


                    oldwhyHexaElement.Text = whyHexaElement.Text;
                    oldwhyHexaElement.whyHexa = whyHexaElement.whyHexa;
                    oldwhyHexaElement.WhyHexaId = whyHexaElement.WhyHexaId;


                    _context.Update(oldwhyHexaElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhyHexaElementExists(whyHexaElement.Id))
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
            ViewData["WhyHexaId"] = new SelectList(_context.WhyHexas, "Id", "Id", whyHexaElement.WhyHexaId);
            return View(whyHexaElement);
        }

        // GET: Admin/WhyHexaElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyHexaElement = await _context.WhyHexaElements
                .Include(w => w.whyHexa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyHexaElement == null)
            {
                return NotFound();
            }

            return View(whyHexaElement);
        }

        // POST: Admin/WhyHexaElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var whyHexaElement = await _context.WhyHexaElements.FirstOrDefaultAsync(b => b.Id == id);
            if (whyHexaElement == null)
            {
                return NotFound();
            }

            // Delete the associated file if it exists
            if (whyHexaElement.IconName != null)
            {
                var existingFilePath = Path.Combine(_env.WebRootPath, "AllFiles/whyHexaElementFiles", whyHexaElement.IconName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            _context.WhyHexaElements.Remove(whyHexaElement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool WhyHexaElementExists(int id)
        {
            return _context.WhyHexaElements.Any(e => e.Id == id);
        }
    }
}
