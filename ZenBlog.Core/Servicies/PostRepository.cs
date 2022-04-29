using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.Common;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Context;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Servicies
{
    public class PostRepository : IPostRepository
    {
        private ZenBlogContext _context;
        public PostRepository(ZenBlogContext context)
        {
            _context = context;
        }

        public async Task Delete(Post post)
        {
            new IOImage().DeleteImage(post.Image);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int postId)
        {
            var post = await GetPostById(postId);
            await Delete(post);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<GetAllPostViewModel> GetAllPost(int pageNumber = 0, int pageSize = 5)
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var countPage = posts.Count() / pageSize;
            return new GetAllPostViewModel()
            {
                Posts = posts,
                CountPage = countPage
            };
        }

        public async Task<GetAllPostViewModel> GetStudentPosts(int studentId, int pageNumber = 0, int pageSize = 5)
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.UserId == studentId)
                .OrderByDescending(p => p.Id)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var countPage = posts.Count() / pageSize;
            return new GetAllPostViewModel()
            {
                Posts = posts,
                CountPage = countPage
            };
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<AddEditPostViewModel> GetPostForUpdate(int postId)
        {
            var result = await _context.Posts
                .Select(p => new AddEditPostViewModel()
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    Category = _context.Categories.ToList(),
                    UserId = p.UserId,
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    Description = p.Description,
                    OldImage = p.Image,
                    Publish = p.Publish,
                    ShowInSlider = p.ShowInSlider
                })
                .SingleOrDefaultAsync(p => p.Id == postId);
            return result;
        }

        public async Task<IEnumerable<Post>> GetPostSlider()
        {
            return _context.Posts
                .Include(p => p.Category)
                .Where(p => p.ShowInSlider && p.Publish);
        }

        public async Task Insert(AddEditPostViewModel post)
        {
            IOImage iOImage = new();
            var imageName = await iOImage.SaveImage(post.Image, IOImage.ImageMode.post);

            Post post1 = new()
            {
                //Id = post.Id,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Description = post.Description,
                Image = imageName,
                Publish = post.Publish,
                ShowInSlider = post.ShowInSlider
            };
            await _context.Posts.AddAsync(post1);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> SearchPost(string text)
        {
            return await _context.Posts
                .Where(p => (
                p.Title.Contains(text) ||
                p.ShortDescription.Contains(text) ||
                p.Description.Contains(text))
                && p.Publish
                )
                .ToListAsync();
        }

        public async Task Update(AddEditPostViewModel post)
        {
            IOImage iOImage = new();
            var imageName = post.OldImage;
            if (post.Image != null)
            {
                imageName = await iOImage.SaveImage(post.Image, IOImage.ImageMode.post);
                iOImage.DeleteImage(post.OldImage);
            }

            Post post1 = new()
            {
                Id = post.Id,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Description = post.Description,
                Image = imageName,
                Publish = post.Publish,
                ShowInSlider = post.ShowInSlider
            };
            _context.Posts.Update(post1);
            await _context.SaveChangesAsync();
        }
    }
}
