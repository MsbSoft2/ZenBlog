

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Context;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Servicies
{
    public class CategoryRepository : ICategoryRepository
    {
        private ZenBlogContext _context;
        public CategoryRepository(ZenBlogContext context)
        {
            _context = context;
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            await Delete(category);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<GetCategoryViewModel> GetCategory(int categoryId, int pageNumber = 0, int pageSize = 10)
        {
            var posts = _context.Posts
                .Include(p => p.User)
                .Where(p => p.CategoryId == categoryId);

            posts = posts.Skip(pageNumber * pageSize).Take(pageSize);

            var postCount = await posts.CountAsync() / pageSize;

            return new GetCategoryViewModel()
            {
                PostCount = postCount,
                Posts = posts
            };
        }

        public async Task<IEnumerable<Category>> GetCategoryForIndex()
        {
            var result = await _context.Categories
                .Include(c =>
                    c.Posts
                    .Where(p => p.Publish)
                    .Take(10))
                .ThenInclude(c => c.User)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            return result;
        }

        public async Task Insert(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
