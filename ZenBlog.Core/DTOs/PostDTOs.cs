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
    public class AddEditPostViewModel
    {
        /*
        Id,CategoryId,UserId,Title,ShortDescription,Description,Image,OldImage,Publish,ShowInSlider
        */
        public int Id { get; set; }

        [Display(Name = "گروه")]
        public int CategoryId { get; set; }

        public int UserId { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300)]
        public string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "متن اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }

        [Display(Name = "تصویر قبلی")]
        public string OldImage { get; set; }

        [Display(Name = "انتشار")]
        public bool Publish { get; set; }

        [Display(Name = "نمایش در اسلایدر")]
        public bool ShowInSlider { get; set; }

        [Display(Name = "گروه ها")]
        public IEnumerable<Category> Category { get; set; }
    }

    public class GetPostViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CategoryTitle { get; set; }
        public int UserId { get; set; }
        public int WriterFullName { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300)]
        public string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "متن اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile Image { get; set; }

        [Display(Name = "تصویر قبلی")]
        public string OldImage { get; set; }

        [Display(Name = "انتشار")]
        public bool Publish { get; set; }

        public List<Comment> Comments { get; set; }
    }
    public class GetAllPostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public int CountPage { get; set; }
    }
}
