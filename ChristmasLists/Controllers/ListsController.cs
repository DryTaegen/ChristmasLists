using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChristmasLists.Data;
using ChristmasLists.Models;

namespace ChristmasLists.Controllers
{
    public class ListsController : Controller
    {
        private readonly ChristmasListsContext _context;

        public ListsController(ChristmasListsContext context)
        {
            _context = context;
        }

        // GET: Lists

        public async Task<IActionResult> Index(string recipientName, string searchString)
        {
            if (_context.Lists == null)
            {
                return Problem("Entity set 'Christmas lists'  is null.");
            }

            IQueryable<string> recipientQuery = from m in _context.Lists
                                                orderby m.Recipient
                                                select m.Recipient;


            var items = from i in _context.Lists
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.giver!.Contains(searchString) || s.gift!.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(recipientName))
            {
                items = items.Where(x => x.Recipient == recipientName);
            }

            var RecipientVM = new RecipientViewModel
            {
                Recipient = new SelectList(await recipientQuery.Distinct().ToListAsync()),
                Lists = await items.ToListAsync()
            };

            return View(RecipientVM);
        }

            // GET: Lists/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lists == null)
            {
                return NotFound();
            }

            return View(lists);
        }

        // GET: Lists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Recipient,year,gift,giver")] Lists lists)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lists);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lists);
        }


        // GET: Lists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists.FindAsync(id);
            if (lists == null)
            {
                return NotFound();
            }
            return View(lists);
        }

        // POST: Lists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recipient,year,gift,giver")] Lists lists)
        {
            if (id != lists.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListsExists(lists.Id))
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
            return View(lists);
        }

        // GET: Lists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lists == null)
            {
                return NotFound();
            }

            return View(lists);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lists == null)
            {
                return Problem("Entity set 'ChristmasListsContext.Lists'  is null.");
            }
            var lists = await _context.Lists.FindAsync(id);
            if (lists != null)
            {
                _context.Lists.Remove(lists);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListsExists(int id)
        {
          return (_context.Lists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
