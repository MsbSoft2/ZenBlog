using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.Repositories;
using ZenBlog.DataLayer.Context;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Servicies
{
    public class CommentRepository : ICommentRepository
    {
        private ZenBlogContext _context;
        public CommentRepository(ZenBlogContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task Insert(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
