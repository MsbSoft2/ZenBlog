using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.DataLayer.Models;

namespace ZenBlog.Core.DTOs
{
    public class GetAllUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public int PageCount { get; set; }
    }
    public class AddUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string phone { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "نقش")]
        public UserRole Role { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [Display(Name = "تصویر جدید")]
        public IFormFile Image { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string phone { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "نقش")]
        public UserRole Role { get; set; }

        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "تصویر قبلی")]
        public string OldImage { get; set; }

        [Display(Name = "تصویر جدید")]
        public IFormFile Image { get; set; }
    }
}
