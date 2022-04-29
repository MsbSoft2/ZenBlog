using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.DTOs
{
    public class GetCategoryViewModel
    {
        public int PostCount { get; set; }

        public IEnumerable<Post> Posts { get; set; }

    }

}
