using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Entities;

namespace DuaControl.Web.Controllers
{
    public class PuertosController : Controller
    {
        private readonly DataContext _context;

        public PuertosController(DataContext context)
        {
            _context = context;
        }

        // GET: Puertos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Puertos.ToListAsync());
        }

        // GET: Puertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puerto = await _context.Puertos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puerto == null)
            {
                return NotFound();
            }

            return View(puerto);
        }

        // GET: Puertos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Puertos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Puerto puerto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puerto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puerto);
        }

        // GET: Puertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puerto = await _context.Puertos.FindAsync(id);
            if (puerto == null)
            {
                return NotFound();
            }
            return View(puerto);
        }

        // POST: Puertos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Puerto puerto)
        {
            if (id != puerto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puerto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuertoExists(puerto.Id))
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
            return View(puerto);
        }

        // GET: Puertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puerto = await _context.Puertos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puerto == null)
            {
                return NotFound();
            }

            return View(puerto);
        }

        // POST: Puertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puerto = await _context.Puertos.FindAsync(id);
            _context.Puertos.Remove(puerto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuertoExists(int id)
        {
            return _context.Puertos.Any(e => e.Id == id);
        }
    }
}
