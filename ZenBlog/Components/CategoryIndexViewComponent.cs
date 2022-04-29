using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Components
{
    public class CategoryIndexViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoryIndexViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetCategoryForIndex();
            return View("/Views/Components/CategoryIndexViewComponent.cshtml", categories);
        }
    }
}