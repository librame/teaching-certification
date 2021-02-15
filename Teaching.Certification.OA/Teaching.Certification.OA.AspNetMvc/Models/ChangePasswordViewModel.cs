using System.ComponentModel.DataAnnotations;

namespace Teaching.Certification.OA.AspNetMvc
{
    public class ChangePasswordViewModel
    {
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "新密码长度须保持在6~20以内")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "两次新密码不一致")]
        [DataType(DataType.Password)]
        public string ReNewPassword { get; set; }
    }
}
