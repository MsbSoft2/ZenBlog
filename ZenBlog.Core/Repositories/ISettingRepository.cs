using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Core.DTOs;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.Repositories
{
    public interface ISettingRepository : IAsyncDisposable
    {
        Task<ShowCountViewModel> ShowCount();
        Task<Setting> GetSetting();
        Task<bool> ChangeSetting(Setting setting);

    }
}
