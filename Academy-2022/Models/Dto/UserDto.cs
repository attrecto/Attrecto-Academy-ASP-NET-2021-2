using System.ComponentModel.DataAnnotations;

namespace Academy_2022.Models.Dto
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
