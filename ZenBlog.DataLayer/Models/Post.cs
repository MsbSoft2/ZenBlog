
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenBlog.DataLayer.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }


        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        public bool Publish { get; set; }
        public bool ShowInSlider { get; set; }


        public User User { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }


    }
}
