namespace Academy_2022.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        // N - 1 relation
        public int? AuthorId { get; set; }
        public User Author { get; set; }
    }
}
