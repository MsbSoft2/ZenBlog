using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICategoryRepository _categoryRepository;
        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: PostController
        [Route("[area]/Posts/{pageNumber?}")]
        public async Task<ActionResult> Index(int pageNumber = 0, int pageSize = 5)
        {
            ViewData["pageNumber"] = pageNumber;
            return View(await _postRepository.GetAllPost(pageNumber, pageSize));
        }

        // GET: PostController/Details/5
        //public async Task<ActionResult> Details(int id)
        //{
        //    return View(await _postRepository.GetPostById(id));
        //}

        // GET: PostController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();
            return View(new AddEditPostViewModel()
            {
                Category = categories
            });
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddEditPostViewModel addEditPost)
        {
            if (!ModelState.IsValid)
            {
                return View(addEditPost);
            }
            addEditPost.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postRepository.Insert(addEditPost);
            return RedirectToAction(nameof(Index));
        }

        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _postRepository.GetPostForUpdate(id));
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AddEditPostViewModel addEditPost)
        {
            if (!ModelState.IsValid)
            {
                return View(addEditPost);
            }
            await _postRepository.Update(addEditPost);
            return RedirectToAction(nameof(Index));
        }

        // GET: PostController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _postRepository.GetPostById(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(float id)
        {
            await _postRepository.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
