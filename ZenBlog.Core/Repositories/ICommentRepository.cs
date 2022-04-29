using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Repositories
{
    public interface ICommentRepository : IAsyncDisposable
    {
        Task Insert(Comment comment);
    }
}
