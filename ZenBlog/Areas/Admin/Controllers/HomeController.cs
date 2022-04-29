using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZenBlog.Core.Common;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private ISettingRepository _settingRepository;
        private IUserRepository _userRepository;

        public HomeController(ISettingRepository settingRepository, IUserRepository userRepository)
        {
            _settingRepository = settingRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _settingRepository.ShowCount());
        }

        // GET: UserController/Profile/5
        public async Task<ActionResult> Profile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userRepository.GetById(userId);
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

        // POST: UserController/Profile/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile([Bind("FullName,phone,Email,Password,OldImage,Image")] EditUserViewModel editUser)
        {
            /*
             Id,FullName,phone,Email,Role,Password,OldImage,Image
             */
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }

            await _userRepository.Update(editUser);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Setting() => View(await _settingRepository.GetSetting());

        [HttpPost]
        public async Task<IActionResult> Setting(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }
            var result =await _settingRepository.ChangeSetting(setting);
            if (!result)
            {
                ModelState.AddModelError("AboutUs", "اطلاعات را به درستی وارد کنید");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
