using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Route("Category/{categoryTitle}/{categoryId}/{pageNumber?}")]
        public async Task<IActionResult> ShowCategory(string categoryTitle, int categoryId, int pageNumber = 0, int pageSize = 5)
        {
            ViewData["categoryTitle"] = categoryTitle;
            var result = await _categoryRepository
                .GetCategory(categoryId, pageNumber, pageSize);
            return View(result);
        }
    }
}
