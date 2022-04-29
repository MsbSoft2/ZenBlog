using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        public PostController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        [Route("/Post/{categoryTitle}/{postTitle}/{postId}")]
        public async Task<IActionResult> Post(string postTitle, int postId, string categoryTitle, bool commentAdded)
        {
            ViewData["commentAdded"] = commentAdded;
            ViewData["categoryTitle"] = categoryTitle;
            ViewData["postTitle"] = postTitle;
            var result = await _postRepository.GetPostById(postId);
            if (!result.Publish)
            {
                return View(null);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult PostComment(
            string fullName, string email, string message,
            string postTitle, int postId, string categoryTitle)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(message))
            {
                return RedirectToAction("Post", new
                {
                    postTitle = postTitle,
                    postId = postId,
                    categoryTitle = categoryTitle
                });
            }

            _commentRepository.Insert(new Comment()
            {
                FullName = fullName,
                Email = email,
                Message = message,
                PostId = postId
            });
            return RedirectToAction("Post", new
            {
                postTitle = postTitle,
                postId = postId,
                categoryTitle = categoryTitle,
                commentAdded = true
            });
        }
    }
}
