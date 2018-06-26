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
        // GET: Permissions/AddToRoles/5
        public ActionResult LinkRoles(int appId)
        {
            if (appId == 0)
            {
                return NotFound();
            }

            //Get all permissions and roles for application + all the currently selected permissions in roles
            PermissionsToRolesViewModel permsRolesVM = new PermissionsToRolesViewModel();
            permsRolesVM.ApplicationId = appId;
            permsRolesVM.PossiblePermissions = _context.Permission.Where(s => s.ApplicationId == appId && s.Deleted != true).ToList();
            permsRolesVM.PossibleRoles = _context.Role.Where(s => s.ApplicationId == appId && s.Deleted != true).ToList();
            permsRolesVM.ExistingRolePermissions = _context.RolePermissions.Where(s => s.ApplicationId == appId).ToList();
            return View(permsRolesVM);
        }

        // POST: Permissions/AddToRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkRoles([Bind("ApplicationId,CurrentPermissionId,CurrentRoleIds")] PermissionsToRolesViewModel permsRolesvm)
        {
            try
            {
                //TODO - should I wipe these out or figure out if the relationship exists then delete the ones not there? RUN It by Doug
                // - NEEDs to be async call to SAVE 
                // - ASYNC reader on DDL change to show which ones were selected already
                // - REUSABLE pattern for thigns down the line

                //TODO - this should ONLY be an ASYNC call and therefore not need a redicrect in it's final version
                return Ok("Saved successfully!");
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}