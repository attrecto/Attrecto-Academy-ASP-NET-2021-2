using System.ComponentModel.DataAnnotations;

namespace Academy_2022.Models.Dto
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
