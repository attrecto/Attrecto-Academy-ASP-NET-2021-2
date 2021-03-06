namespace Academy_2022.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string? Name { get; set; }

        public List<MinimalCourseDto>? Courses { get; set; }
    }
}
