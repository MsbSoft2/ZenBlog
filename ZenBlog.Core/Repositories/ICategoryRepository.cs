using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Repositories
{
    public interface ICategoryRepository : IAsyncDisposable
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<GetCategoryViewModel> GetCategory(int categoryId, int pageNumber = 0, int pageSize = 10);
        Task Insert(Category category);
        Task Update(Category category);
        Task Delete(Category category);
        Task Delete(int categoryId);

        Task<IEnumerable<Category>> GetCategoryForIndex();
    }
}
