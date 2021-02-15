using System.ComponentModel.DataAnnotations;

namespace Teaching.Certification.OA.AspNetMvc
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度须保持在6~20以内")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "两次密码不一致")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        public bool IsMale { get; set; }
    }
}
