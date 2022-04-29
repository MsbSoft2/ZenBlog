
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenBlog.DataLayer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string Title { get; set; }

        public List<Post> Posts { get; set; }
    }
}
