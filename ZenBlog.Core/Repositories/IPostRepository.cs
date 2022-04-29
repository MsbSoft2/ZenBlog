using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Repositories
{
    public interface IPostRepository : IAsyncDisposable
    {
        Task<GetAllPostViewModel> GetAllPost(int pageNumber = 0, int pageSize = 5);
        Task<GetAllPostViewModel> GetStudentPosts(int studentId, int pageNumber = 0, int pageSize = 5);
        Task<IEnumerable<Post>> SearchPost(string text);
        Task<IEnumerable<Post>> GetPostSlider();



        Task<AddEditPostViewModel> GetPostForUpdate(int postId);
        Task Insert(AddEditPostViewModel post);
        Task Update(AddEditPostViewModel post);
        Task<Post> GetPostById(int postId);
        Task Delete(Post post);
        Task Delete(int postId);
    }
}
