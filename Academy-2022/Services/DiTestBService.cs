namespace Academy_2022.Services
{
    public class DiTestBService : IDiTestBService
    {
        private readonly IDiTestAService _diTestAService;

        public DiTestBService(IDiTestAService diTestAService)
        {
            _diTestAService = diTestAService;
            Console.WriteLine("..");
        }

        public int Test()
        {
            return 1;
        }
    }
}
