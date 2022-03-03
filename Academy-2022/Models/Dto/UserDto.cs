using System.ComponentModel.DataAnnotations;

namespace Academy_2022.Models.Dto
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
