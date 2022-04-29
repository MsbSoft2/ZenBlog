
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenBlog.DataLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(150)]
        public string phone { get; set; }

        [Required]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserRole Role { get; set; }

        [Required]
        public string Password { get; set; }

        public string Image { get; set; }


        public IEnumerable<Post> Posts { get; set; }
    }

    public enum UserRole
    {
        admin,
        teacher,
        student
    }
}
