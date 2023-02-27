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
using Microsoft.AspNetCore.Http;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SocialsController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;


        public SocialsController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Socials
        public async Task<IActionResult> Index()
        {
            var hexaConDbContext = _context.Socials.Include(s => s.footer);
            return View(await hexaConDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveSocials(IFormCollection formdata)
        {
            try
            {
                var ids = formdata["Id"].ToString().Split(',');
                var files = Request.Form.Files;
                List<Social> socials = new List<Social>();
                for (int i = 0; i < ids.Length; i++)
                {
                    socials.Add(new Social
                    {
                        Url = formdata.Any(c => c.Key == $"Url{i}") ? formdata.FirstOrDefault(c => c.Key == $"Url{i}").Value.ToString() : "",
                        Id = int.Parse(ids[i]),
                        File = files.Any(c => c.Name == $"File{i}") ? files.FirstOrDefault(c => c.Name == $"File{i}") : null
                    });
                }

                foreach (var social in socials?.Where(c => c.Id != 0))
                {
                    var oldSocial = await _context.Socials.FirstOrDefaultAsync(b => b.Id == social.Id);

                    if (social.File != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(social.File.FileName);
                        string name = social.File.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/SocialFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await social.File.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldSocial.IconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldSocial.IconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldSocial.IconName = fileName;
                        oldSocial.OriginalName = name;
                    }


                    oldSocial.Url = social.Url;

                    _context.Update(oldSocial);
                    await _context.SaveChangesAsync();

                }





                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Socials updates saved." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred while saving the Socials updates: " + ex.Message });
            }
        }


        // GET: Admin/Socials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var social = await _context.Socials
                .Include(s => s.footer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (social == null)
            {
                return NotFound();
            }

            return View(social);
        }

        // GET: Admin/Socials/Create
        public IActionResult Create()
        {
            ViewData["FooterId"] = new SelectList(_context.Footers, "Id", "Id");
            return View();
        }

        // POST: Admin/Socials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Social social)
        {
            if (ModelState.IsValid)
            {
                if (social.File != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(social.File.FileName);
                    string name = social.File.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/SocialFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await social.File.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    social.IconName = fileName;
                    social.OriginalName = name;
                }
                _context.Add(social);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FooterId"] = new SelectList(_context.Footers, "Id", "Id", social.FooterId);
            return View(social);
        }

        // GET: Admin/Socials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var social = await _context.Socials.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            ViewData["FooterId"] = new SelectList(_context.Footers, "Id", "Id", social.FooterId);
            return View(social);
        }

        // POST: Admin/Socials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Social social)
        {
            if (id != social.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var oldSocial = await _context.Socials.FirstOrDefaultAsync(b => b.Id == social.Id);

                    if (social.File != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(social.File.FileName);
                        string name = social.File.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/SocialFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await social.File.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldSocial.IconName != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldSocial.IconName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldSocial.IconName = fileName;
                        oldSocial.OriginalName = name;
                    }


                    oldSocial.Url = social.Url;

                    _context.Update(oldSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialExists(social.Id))
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
            ViewData["FooterId"] = new SelectList(_context.Footers, "Id", "Id", social.FooterId);
            return View(social);
        }

        // GET: Admin/Socials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var social = await _context.Socials
                .Include(s => s.footer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (social == null)
            {
                return NotFound();
            }

            return View(social);
        }

        // POST: Admin/Socials/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }

            string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/SocialFiles");
            var filePath = Path.Combine(rootPath, social.IconName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SocialExists(int id)
        {
            return _context.Socials.Any(e => e.Id == id);
        }
    }
}
