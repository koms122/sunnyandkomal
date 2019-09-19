using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunnyandKomal.Data;
using SunnyandKomal.Models;

namespace SunnyandKomal.Controllers
{
    public class GuestBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GuestBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.GuestBook.ToListAsync());
        }

        // GET: GuestBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBook
                .FirstOrDefaultAsync(m => m.ID == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // GET: GuestBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Message,DateCreated")] GuestBook guestBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBook.FindAsync(id);
            if (guestBook == null)
            {
                return NotFound();
            }
            return View(guestBook);
        }

        // POST: GuestBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Message,DateCreated")] GuestBook guestBook)
        {
            if (id != guestBook.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookExists(guestBook.ID))
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
            return View(guestBook);
        }

        // GET: GuestBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBook
                .FirstOrDefaultAsync(m => m.ID == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // POST: GuestBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestBook = await _context.GuestBook.FindAsync(id);
            _context.GuestBook.Remove(guestBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookExists(int id)
        {
            return _context.GuestBook.Any(e => e.ID == id);
        }
    }
}
