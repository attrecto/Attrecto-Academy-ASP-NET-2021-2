using System.ComponentModel.DataAnnotations;

namespace Academy_2022.Models.Dto
{
    public class UpdateUserDto : CreateUserDto
    {
        public int Id { get; set; }

        public List<int> Courses { get; set; }
    }
}
