﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TurističkaAgencija.Models;

namespace TurističkaAgencija.Controllers
{
    public class DestinacijaController : Controller
    {
        private readonly TuristickaAgencijaContext _context;

        public DestinacijaController(TuristickaAgencijaContext context)
        {
            _context = context;
        }

        // GET: Destinacija
        public async Task<IActionResult> Index()
        {
            var turistickaAgencijaContext = _context.Destinacija.Include(d => d.Drzava);
            return View(await turistickaAgencijaContext.ToListAsync());
        }

        // GET: Destinacija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _context.Destinacija
                .Include(d => d.Drzava)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // GET: Destinacija/Create
        public IActionResult Create()
        {
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "Id", "Naziv");
            return View();
        }

        // POST: Destinacija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DrzavaId,Grad,Opis,Slika")] Destinacija destinacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destinacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "Id", "Naziv", destinacija.DrzavaId);
            return View(destinacija);
        }

        // GET: Destinacija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _context.Destinacija.FindAsync(id);
            if (destinacija == null)
            {
                return NotFound();
            }
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "Id", "Naziv", destinacija.DrzavaId);
            return View(destinacija);
        }

        // POST: Destinacija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DrzavaId,Grad,Opis,Slika")] Destinacija destinacija)
        {
            if (id != destinacija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destinacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinacijaExists(destinacija.Id))
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
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "Id", "Naziv", destinacija.DrzavaId);
            return View(destinacija);
        }

        // GET: Destinacija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _context.Destinacija
                .Include(d => d.Drzava)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // POST: Destinacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destinacija = await _context.Destinacija.FindAsync(id);
            _context.Destinacija.Remove(destinacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinacijaExists(int id)
        {
            return _context.Destinacija.Any(e => e.Id == id);
        }
    }
}
