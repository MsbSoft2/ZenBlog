using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenBlog.Core.DTOs
{
    public class ShowCountViewModel
    {
        public int UserCount { get; set; }
        public int CategoryCount { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
    }
}
