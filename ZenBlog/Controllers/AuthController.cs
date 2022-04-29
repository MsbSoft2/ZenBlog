using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Controllers
{
    public class AuthController : Controller
    {
        private IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Login")]
        public IActionResult Login() => View();

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userRepository.Login(login);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربر با این اطلاعات یافت نشد");
                return View();
            }
            #region Authentication

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.MobilePhone, user.phone),
                new Claim(ClaimTypes.Hash, user.Password),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Actor, user.Image),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RemmeberMe
            };

            await HttpContext.SignInAsync(principle, properties);

            #endregion

            return Redirect("/");
        }

        [HttpGet("Register")]
        public IActionResult Register() => View();

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var result = await _userRepository.Insert(new AddUserViewModel()
            {
                FullName = register.FullName,
                Email = register.Email,
                Image = null,
                Password = register.Password,
                RePassword = register.RePassword,
                Role = UserRole.student,
                phone = register.phone
            });
            if (result == false)
            {
                ModelState.AddModelError("Email", "کاربری با این اطلاعات وجود دارد");
                return View();
            }
            return RedirectToAction(nameof(Login));
        }


        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
