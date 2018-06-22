using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectWeb.Models.DataModels;

namespace ConnectWeb.Controllers
{
    public class PermissionsController : Controller
    {
        private D3ConnectContext _context;

        public PermissionsController(D3ConnectContext context)
        {
            _context = context;
        }

        // GET: Permissions
        public ActionResult Index(int appId)
        {
            var app = _context.Application.FirstOrDefault(a => a.Id == appId);
            if (app != null)
            {
                var Permissions = _context.Permission.Where(s => s.ApplicationId == appId && s.Deleted == false);
                ViewBag.ApplicationName = app.Name;
                ViewBag.AppId = appId;
                return View(Permissions);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Permissions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Permissions/Create
        public IActionResult Create(int appId)
        {
            Permission newPermission = new Permission();
            newPermission.ApplicationId = appId;
            return View(newPermission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,Name,Description")] Permission Permission)
        {
            if (ModelState.IsValid)
            {
                Permission.PermissionUniqueId = System.Guid.NewGuid();
                Permission.Deleted = false;
                _context.Add(Permission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = Permission.ApplicationId });
            }
            return View(Permission);
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var app = _context.Permission.FirstOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Permissions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] Permission Permission)
        {
            try
            {
                var PermissionFound = _context.Permission.FirstOrDefault(s => s.Id == Permission.Id);
                if (PermissionFound == null)
                {
                    return NotFound();
                }
                PermissionFound.Name = Permission.Name;
                PermissionFound.Description = Permission.Description;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = PermissionFound.ApplicationId });
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

            var Permission = _context.Permission.SingleOrDefault(s => s.Id == id);
            if (Permission == null)
            {
                return NotFound();
            }

            return View(Permission);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Permission = _context.Permission.SingleOrDefault(s => s.Id == id);
            if (Permission == null)
            {
                return NotFound();
            }
            Permission.Deleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { appId = Permission.ApplicationId });
        }
    }
}