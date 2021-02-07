using System.ComponentModel.DataAnnotations;

namespace Teaching.Certification.OA.AspNetMvc
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
