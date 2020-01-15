using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using DuaControl.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DuaControl.Web.Controllers
{
    public class FacturasController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IHostingEnvironment _environment;

        public FacturasController(
            DataContext context,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper,
            IHostingEnvironment environment)
        {
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _environment = environment;
        }

        // GET: Facturas
        public IActionResult Index()
        {
            return View(_context.Facturas
                .Include(f => f.Adjuntos)
                .Include(f => f.Client)
                .Include(f => f.Port));
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var factura = await _context.Facturas.FindAsync(id);
            var factura = await _context.Facturas
                .Include(f => f.Client)
                .Include(f => f.Port)
                .Include(f => f.Adjuntos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToFacturaViewModel(factura));
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FacturaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var factura = await _converterHelper.ToFacturaAsync(model);
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonResult(true);
            }
            model.Puertos = _combosHelper.GetComboPorts();
            return View(model);
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddFileAsync(FacturaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                var path = Path.Combine(_environment.WebRootPath, @"uploads\" + model.InvoiceNumber);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var completePath = Path.Combine(@path, file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                            {
                                //Verify if there's already a file with the same name on this folder.
                                if (Directory.Exists(completePath))
                                {
                                    //Delete the file and then save the new one.
                                    Directory.Delete(completePath);
                                }
                                await file.CopyToAsync(fileStream);

                               var adjunto = new Adjunto
                                {
                                    RegisterDate = DateTime.Now,
                                    User = "gomezjos",
                                    DocumentUrl = completePath,
                                    Factura = await _context.Facturas.FirstOrDefaultAsync(f => f.Id == model.Id)
                                };
                                _context.Adjuntos.Add(adjunto);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                    return new JsonResult(true);
                }
            }
            return RedirectToAction($"Edit/{model.Id}");
            //return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adjunto = await _context.Adjuntos
                .FirstOrDefaultAsync(a => a.Id == id.Value);
            if (adjunto == null)
            {
                return NotFound();
            }
            var fileName = Path.GetFileName(adjunto.DocumentUrl);
            if (Directory.Exists(adjunto.DocumentUrl))
            {
                //Delete the file and then save the new one.
                Directory.Delete(adjunto.DocumentUrl);
            }

            _context.Adjuntos.Remove(adjunto);
            await _context.SaveChangesAsync();
            return new JsonResult(true);
            //return RedirectToAction($"{nameof(DetailsPet)}/{history.Pet.Id}");
        }
    }
}
