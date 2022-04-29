using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBlog.Core.Common;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Context;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Servicies
{
    public class UserRepository : IUserRepository
    {
        private ZenBlogContext _context;
        public UserRepository(ZenBlogContext context)
        {
            _context = context;
        }

        public async Task<GetAllUserViewModel> GetAll(int pageNumber = 0, int pageSize = 5)
        {
            IEnumerable<User> users = await _context.Users
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            var pageCount = users.Count() / pageSize;

            return new GetAllUserViewModel()
            {
                Users = users.Skip(pageNumber * pageSize).Take(pageSize),
                PageCount = pageCount + 1
            };
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> Insert(AddUserViewModel user)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower() || u.phone == user.phone);
            if (result == null)
            {
                IOImage iOImage = new();
                var imageName = await iOImage.SaveImage(user.Image, IOImage.ImageMode.profile);
                User user1 = new()
                {
                    //Id =,
                    FullName = user.FullName,
                    phone = user.phone,
                    Email = user.Email,
                    Role = user.Role,
                    Password = user.Password,
                    Image = imageName
                };
                await _context.Users.AddAsync(user1);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task Update(EditUserViewModel user)
        {
            IOImage iOImage = new();
            var imageName = user.OldImage;
            if (user.Image != null)
            {
                imageName = await iOImage.SaveImage(user.Image, IOImage.ImageMode.profile);
                iOImage.DeleteImage(user.OldImage);
            }
            User user1 = new()
            {
                Id = user.Id,
                FullName = user.FullName,
                phone = user.phone,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                Image = imageName
            };
            _context.Users.Update(user1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int userId)
        {
            var user = await GetById(userId);
            await Delete(user);
        }

        public async Task Delete(User user)
        {
            new IOImage().DeleteImage(user.Image);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<User> Login(LoginViewModel login)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u =>
                u.Email == login.Email.ToLower() &&
                u.Password == login.Password);
        }
    }
}
