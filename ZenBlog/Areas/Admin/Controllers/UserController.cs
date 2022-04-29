using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: UserController
        public async Task<ActionResult> Index(int pageNumber = 0, int pageSize = 10)
        {
            ViewData["pageNumber"] = pageNumber;
            return View(await _userRepository.GetAll(pageNumber, pageSize));
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddUserViewModel addUser)
        {
            if (!ModelState.IsValid)
            {
                return View(addUser);
            }
            var result = await _userRepository.Insert(addUser);
            if (result == false)
            {
                ModelState.AddModelError("Email", "کاربر با اطلاعات یافت شد");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userRepository.GetById(id);
            return View(new EditUserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                phone = user.phone,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                OldImage = user.Image
            });
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,FullName,phone,Email,Role,Password,OldImage,Image")] EditUserViewModel editUser)
        {
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }
            await _userRepository.Update(editUser);
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(float id)
        {
            await _userRepository.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
