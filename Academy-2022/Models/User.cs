namespace Academy_2022.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        // members
        public List<CourseUser> Courses { get; set; }

        public string Role { get; set; }
    }
}
