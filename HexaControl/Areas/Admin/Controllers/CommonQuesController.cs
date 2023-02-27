using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HexaControl.Infustructur;
using HexaControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CommonQuesController : Controller
    {
        private readonly HexaConDbContext _context;

        public CommonQuesController(HexaConDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CommonQues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commons.ToListAsync());
        }

        // GET: Admin/CommonQues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonQues = await _context.Commons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commonQues == null)
            {
                return NotFound();
            }

            return View(commonQues);
        }


        [HttpPost]
        public JsonResult SaveCommens([FromBody] List<CommonQues> commons)
        {
            try
            {
               

                _context.UpdateRange(commons?.Where(c => c.Id != 0));
                _context.AddRange(commons?.Where(c => c.Id == 0));
                _context.SaveChanges();
                return new JsonResult(new { message = "Heroes updates saved." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred while saving the Heroes updates: " + ex.Message });
            }
        }


        // GET: Admin/CommonQues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CommonQues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,PubDate,Answer")] CommonQues commonQues)
        {
            if (ModelState.IsValid)
            {
                commonQues.PubDate = DateTime.Now;
                _context.Add(commonQues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commonQues);
        }

        // GET: Admin/CommonQues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonQues = await _context.Commons.FindAsync(id);
            if (commonQues == null)
            {
                return NotFound();
            }
            return View(commonQues);
        }

        // POST: Admin/CommonQues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,PubDate,Answer")] CommonQues commonQues)
        {
            if (id != commonQues.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commonQues.PubDate = DateTime.Now;

                    _context.Update(commonQues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommonQuesExists(commonQues.Id))
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
            return View(commonQues);
        }

        // GET: Admin/CommonQues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonQues = await _context.Commons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commonQues == null)
            {
                return NotFound();
            }

            return View(commonQues);
        }

        // POST: Admin/CommonQues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commonQues = await _context.Commons.FindAsync(id);
            _context.Commons.Remove(commonQues);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommonQuesExists(int id)
        {
            return _context.Commons.Any(e => e.Id == id);
        }
    }
}
