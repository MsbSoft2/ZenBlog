using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public HeaderViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();
            return View("/Views/Components/HeaderViewComponent.cshtml",categories);
        }
    }
}
