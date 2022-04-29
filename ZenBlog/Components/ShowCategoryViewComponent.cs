using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenBlog.Core.Repositories;

namespace ZenBlog.Components
{
    public class ShowCategoryViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public ShowCategoryViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();
            return View("/Views/Components/ShowCategoryViewComponent.cshtml", categories);
        }
    }
}