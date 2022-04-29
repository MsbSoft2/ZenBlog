using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Repositories
{
    public interface IUserRepository : IAsyncDisposable
    {
        Task<GetAllUserViewModel> GetAll(int pageNumber, int pageSize);
        Task<User> GetById(int id);
        Task<bool> Insert(AddUserViewModel user);
        Task Update(EditUserViewModel user);
        Task Delete(int userId);
        Task Delete(User user);

        Task<User> Login(LoginViewModel login);
    }
}
