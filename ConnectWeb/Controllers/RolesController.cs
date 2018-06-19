using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectWeb.Models.DataModels;

namespace ConnectWeb.Controllers
{
    public class RolesController : Controller
    {
        private D3ConnectContext _context;

        public RolesController(D3ConnectContext context)
        {
            _context = context;
        }

        // GET: Roles
        public ActionResult Index(int appId)
        {
            var app = _context.Application.FirstOrDefault(a => a.Id == appId);
            if (app != null)
            {
                var roles = _context.Role.Where(s => s.ApplicationId == appId && s.Deleted == false);
                ViewBag.ApplicationName = app.Name;
                ViewBag.AppId = appId;
                return View(roles);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Roles/Create
        public IActionResult Create(int appId)
        {
            Role newRole = new Role();
            newRole.ApplicationId = appId;
            return View(newRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,Name,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.RoleId = System.Guid.NewGuid();
                role.Deleted = false;
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = role.ApplicationId });
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var app = _context.Role.FirstOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Roles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] Role role)
        {
            try
            {
                var roleFound = _context.Role.FirstOrDefault(s => s.Id == role.Id);
                if (roleFound == null)
                {
                    return NotFound();
                }
                roleFound.Name = role.Name;
                roleFound.Description = role.Description;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = roleFound.ApplicationId });
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

            var role = _context.Role.SingleOrDefault(s => s.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = _context.Role.SingleOrDefault(s => s.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            role.Deleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { appId = role.ApplicationId });
        }
    }
}