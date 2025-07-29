using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.DataTransferObjects.UserDataTransferObjects
{
    public class UserDto : BaseDto
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }
        
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        
        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمزعبور و تکرار آن یکسان نیست")]
        public string RepeatPassword { get; set; }
        
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        
        public string Code { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
    }
}