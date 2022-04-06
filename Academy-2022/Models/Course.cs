using System.ComponentModel.DataAnnotations.Schema;

namespace Academy_2022.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int AuthorId { get; set; }

        public List<User> Students { get; set; }
        public List<CourseUser> CourseUsers { get; set; }
    }
}
