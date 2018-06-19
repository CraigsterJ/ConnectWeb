using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectWeb.Models.DataModels;

namespace ConnectWeb.Controllers
{
    public class UserController : Controller
    {
        //START HERE!!!!!
        private D3ConnectContext _context;

        public UserController(D3ConnectContext context)
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
        public async Task<IActionResult> Create([Bind("ApplicationId,Name,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = System.Guid.NewGuid();
                user.ApplicationId = user.ApplicationId;
                user.UserName = user.UserName;
                user.FullName = user.FullName;
                _context.Add(User);
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
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] User user)
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

            var User = _context.User.SingleOrDefault(s => s.Id == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var User = _context.User.SingleOrDefault(s => s.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            User.Deleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { appId = User.ApplicationId });
        }
    }
}