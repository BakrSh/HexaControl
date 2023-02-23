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
    public class BlogsController : Controller
    {
        private readonly HexaConDbContext _context;
        private readonly IHostingEnvironment _env;

        public BlogsController(HexaConDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }



        //--------------------//

        [HttpPost]
        public async Task<IActionResult> SaveCheckboxValue(int itemId, bool isChecked)
        {




            var item = await _context.Blogs.FindAsync(itemId);
            item.isChecked = isChecked;
            _context.Update(item);

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
        // GET: Admin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Admin/Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.PubDate = DateTime.Now;
                if (blog.MainFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(blog.MainFile.FileName);
                    string name = blog.MainFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await blog.MainFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    blog.MainName = fileName;
                    blog.MainOriginalName = name;
                }
                if (blog.FirstFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(blog.FirstFile.FileName);
                    string name = blog.FirstFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await blog.FirstFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    blog.firstName = fileName;
                    blog.FirstOriginalName = name;
                }
                if (blog.SecondFile != null)
                {

                    // Get file extension
                    string type = System.IO.Path.GetExtension(blog.SecondFile.FileName);
                    string name = blog.SecondFile.FileName;
                    string fileName = Guid.NewGuid().ToString() + type;


                    string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                    // If directory does not exist, create one
                    if (!Directory.Exists(rootPath))
                        Directory.CreateDirectory(rootPath);

                    string filePath = Path.Combine(rootPath, fileName);


                    using (FileStream FS = new FileStream(filePath, FileMode.Create))
                    {
                        await blog.SecondFile.CopyToAsync(FS);
                        //Close the File Stream
                        FS.Close();
                    }
                    blog.SecondName = fileName;
                    blog.SecondOriginalName = name;
                }
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    blog.PubDate = DateTime.Now;
                    var oldBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id);

                    if (blog.MainFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(blog.MainFile.FileName);
                        string name = blog.MainFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await blog.MainFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldBlog.MainFile != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldBlog.MainName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldBlog.MainName = fileName;
                        oldBlog.MainOriginalName = name;
                    }
                    if (blog.FirstFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(blog.FirstFile.FileName);
                        string name = blog.FirstFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await blog.FirstFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldBlog.FirstFile != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldBlog.firstName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldBlog.firstName = fileName;
                        oldBlog.FirstOriginalName = name;
                    }
                    if (blog.SecondFile != null)
                    {
                        // Get file extension
                        string type = System.IO.Path.GetExtension(blog.SecondFile.FileName);
                        string name = blog.SecondFile.FileName;
                        string fileName = Guid.NewGuid().ToString() + type;

                        string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");

                        // If directory does not exist, create one
                        if (!Directory.Exists(rootPath))
                            Directory.CreateDirectory(rootPath);

                        string filePath = Path.Combine(rootPath, fileName);

                        using (FileStream FS = new FileStream(filePath, FileMode.Create))
                        {
                            await blog.SecondFile.CopyToAsync(FS);
                            //Close the File Stream
                            FS.Close();
                        }


                        // Delete the existing file if it exists
                        if (oldBlog.SecondFile != null)
                        {
                            var existingFilePath = Path.Combine(rootPath, oldBlog.SecondName);
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }


                        oldBlog.SecondName = fileName;
                        oldBlog.SecondOriginalName = name;
                    }
                    oldBlog.MainArticle = blog.MainArticle;
                    oldBlog.isChecked = blog.isChecked;
                    oldBlog.PubDate = blog.PubDate;
                    oldBlog.DeclarativeParagraph = blog.DeclarativeParagraph;
                    oldBlog.FirstParg = blog.FirstParg;
                    oldBlog.SecondParg = blog.SecondParg;
                    _context.Update(oldBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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
            return View(blog);
        }

        // GET: Admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            if (blog.MainFile != null)
            {
                string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");
                var existingFilePath = Path.Combine(rootPath, blog.MainName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            if (blog.FirstFile != null)
            {
                string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");
                var existingFilePath = Path.Combine(rootPath, blog.firstName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            if (blog.SecondFile != null)
            {
                string rootPath = Path.Combine(_env.WebRootPath, "AllFiles/BlogsFiles");
                var existingFilePath = Path.Combine(rootPath, blog.SecondName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
