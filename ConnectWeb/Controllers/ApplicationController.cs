using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectWeb.Models.DataModels;

namespace ConnectWeb.Controllers
{
    public class ApplicationController : Controller
    {
        private D3ConnectContext _context;

        public ApplicationController(D3ConnectContext context)
        {
            _context = context;
        }

        // GET: Application
        public ActionResult Index()
        {
            var apps = _context.Application.Where(s => s.Deleted == false); ;
            return View(apps);
        }

        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Application application)
        {
            if (ModelState.IsValid)
            {
                application.ApplicationId = System.Guid.NewGuid();
                application.Deleted = false;
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var app = _context.Application.FirstOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Application/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] Application application)
        {
            try
            {
                var app = _context.Application.FirstOrDefault(s => s.Id == application.Id);
                if (app == null)
                {
                    return NotFound();
                }
                app.Name = application.Name;
                app.Description = application.Description;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _context.Application.SingleOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var app = _context.Application.SingleOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            app.Deleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}