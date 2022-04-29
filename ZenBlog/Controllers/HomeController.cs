using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.Repositories;
using ZenBlog.Models;

namespace ZenBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostRepository _postRepository;
        private ISettingRepository _settingRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ISettingRepository settingRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
            _settingRepository = settingRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel()
            {
                Posts = await _postRepository.GetPostSlider(),
            });
        }

        [Route("AboutUs")]
        public async Task<IActionResult> AboutUs()
        {
            return View(await _settingRepository.GetSetting());
        }

        [Route("ContactUs")]
        public async Task<IActionResult> ContactUsAsync()
        {
            return View(await _settingRepository.GetSetting());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
