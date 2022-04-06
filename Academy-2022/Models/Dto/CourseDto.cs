namespace Academy_2022.Models.Dto
{
    public class CourseDto : MinimalCourseDto
    {
        public List<MinimalUserDto>? Students { get; set; }
    }
}
