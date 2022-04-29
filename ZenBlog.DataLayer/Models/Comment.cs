using System.ComponentModel.DataAnnotations;

namespace ZenBlog.DataLayer.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Message { get; set; }

        public Post Post { get; set; }

    }
}
