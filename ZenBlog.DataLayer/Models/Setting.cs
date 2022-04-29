using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenBlog.DataLayer.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "متن درباره ما")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AboutUs { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(80)]
        public string Address { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        public string Phones { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Image { get; set; }
    }

}
