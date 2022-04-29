using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZenBlog.Core.DTOs;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Context;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Servicies
{
    public class SettingRepository : ISettingRepository
    {
        private ZenBlogContext _context;
        public SettingRepository(ZenBlogContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<ShowCountViewModel> ShowCount()
        {
            return new ShowCountViewModel()
            {
                UserCount = await _context.Users.CountAsync(),
                PostCount = await _context.Posts.CountAsync(),
                CategoryCount = await _context.Categories.CountAsync(),
                CommentCount = await _context.Comments.CountAsync()
            };
        }

        public async Task<Setting> GetSetting()
        {
            return await _context.Settings.SingleOrDefaultAsync(s => s.Id == 1);
        }

        public async Task<bool> ChangeSetting(Setting setting)
        {
            var result = _context.Settings.Update(setting);
            await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
