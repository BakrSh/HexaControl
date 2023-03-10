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
    public class HowWeWorkElementsController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;


        public HowWeWorkElementsController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/HowWeWorkElements
        public async Task<IActionResult> Index()
        {
            var hexaConDbContext = _context.howWeWorks.Include(h => h.howWork);
            return View(await hexaConDbContext.ToListAsync());
        }

        // GET: Admin/HowWeWorkElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWeWorkElement = await _context.howWeWorks
                .Include(h => h.howWork)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (howWeWorkElement == null)
            {
                return NotFound();
            }

            return View(howWeWorkElement);
        }

        // GET: Admin/HowWeWorkElements/Create
        public IActionResult Create()
        {
            
            ViewData["HowWorkId"] = new SelectList(_context.HowWorks, "Id", "Id");
            return View();
        }

        // POST: Admin/HowWeWorkElements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( HowWeWorkElement howWeWorkElement)
        {
            if (ModelState.IsValid)
            {

                if (howWeWorkElement.IconFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(howWeWorkElement.IconFile.FileName);
                    string name = howWeWorkElement.IconFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/howWeWorkElementFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await howWeWorkElement.IconFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    howWeWorkElement.IconName = fileName;
                    howWeWorkElement.OriginalName = name;
                }

                _context.Add(howWeWorkElement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
       
            ViewData["HowWorkId"] = new SelectList(_context.HowWorks, "Id", "Id", howWeWorkElement.HowWorkId);
            return View(howWeWorkElement);
        }

        // GET: Admin/HowWeWorkElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWeWorkElement = await _context.howWeWorks.FindAsync(id);
            if (howWeWorkElement == null)
            {
                return NotFound();
            }
            ViewData["HowWorkId"] = new SelectList(_context.HowWorks, "Id", "Id", howWeWorkElement.HowWorkId);
            return View(howWeWorkElement);
        }

        // POST: Admin/HowWeWorkElements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  HowWeWorkElement howWeWorkElement)
        {
            if (id != howWeWorkElement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var oldhowWeWorkElement = await _context.howWeWorks.FirstOrDefaultAsync(b => b.Id == howWeWorkElement.Id);

                    if (howWeWorkElement.IconFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(howWeWorkElement.IconFile.FileName);
                        string name = howWeWorkElement.IconFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/howWeWorkElementFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await howWeWorkElement.IconFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldhowWeWorkElement.IconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldhowWeWorkElement.IconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldhowWeWorkElement.IconName = fileName;
                        oldhowWeWorkElement.OriginalName = name;
                    }


                    oldhowWeWorkElement.Text = howWeWorkElement.Text;
                    oldhowWeWorkElement.howWork = howWeWorkElement.howWork;
                    oldhowWeWorkElement.HowWorkId = howWeWorkElement.HowWorkId;




                    _context.Update(oldhowWeWorkElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HowWeWorkElementExists(howWeWorkElement.Id))
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
            ViewData["HowWorkId"] = new SelectList(_context.HowWorks, "Id", "Id", howWeWorkElement.HowWorkId);
            return View(howWeWorkElement);
        }

        // GET: Admin/HowWeWorkElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howWeWorkElement = await _context.howWeWorks
                .Include(h => h.howWork)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (howWeWorkElement == null)
            {
                return NotFound();
            }

            return View(howWeWorkElement);
        }

        // POST: Admin/HowWeWorkElements/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var howWeWorkElement = await _context.howWeWorks.FindAsync(id);
            if (howWeWorkElement == null)
            {
                return NotFound();
            }

            // Delete the file
            if (howWeWorkElement.IconName != null)
            {
                string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/howWeWorkElementFiles");
                var existingFilePath = Path.Combine(rootPath, howWeWorkElement.IconName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            _context.howWeWorks.Remove(howWeWorkElement);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool HowWeWorkElementExists(int id)
        {
            return _context.howWeWorks.Any(e => e.Id == id);
        }
    }
}
