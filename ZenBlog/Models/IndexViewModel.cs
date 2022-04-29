using System;
using System.Collections.Generic;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}