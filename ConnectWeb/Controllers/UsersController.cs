using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectWeb.Models.DataModels;

namespace ConnectWeb.Controllers
{
    public class UsersController : Controller
    {
        private D3ConnectContext _context;

        public UsersController(D3ConnectContext context)
        {
            _context = context;
        }

        // GET: Users
        public ActionResult Index(int appId)
        {
            var app = _context.Application.FirstOrDefault(a => a.Id == appId);
            if (app != null)
            {
                var Users = _context.User.Where(s => s.ApplicationId == appId && s.Deleted == false);
                ViewBag.ApplicationName = app.Name;
                ViewBag.AppId = appId;
                return View(Users);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public IActionResult Create(int appId)
        {
            User newUser = new User();
            newUser.ApplicationId = appId;
            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,UserName,FullName")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserUniqueId = System.Guid.NewGuid();
                user.Deleted = false;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = user.ApplicationId });
            }
            return View(User);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var app = _context.User.FirstOrDefault(s => s.Id == id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,UserName,FullName")] User user)
        {
            try
            {
                var userFound = _context.User.FirstOrDefault(s => s.Id == user.Id);
                if (userFound == null)
                {
                    return NotFound();
                }
                userFound.UserName = user.UserName;
                userFound.FullName = user.FullName;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { appId = userFound.ApplicationId });
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

            var user = _context.User.SingleOrDefault(s => s.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _context.User.SingleOrDefault(s => s.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Deleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { appId = user.ApplicationId });
        }
    }
}