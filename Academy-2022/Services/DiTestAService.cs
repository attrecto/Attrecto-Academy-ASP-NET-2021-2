namespace Academy_2022.Services
{
    public class DiTestAService : IDiTestAService
    {
        public int Age { get; set; }

        public DiTestAService()
        {
            Age = 0;
        }

        public void SetAge(int age)
        {
            Age = age;
        }
    }
}
