using Academy_2022.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDiTestAService _diTestAService;
        private readonly IDiTestBService _diTestBService;

        public TestController(
            IDiTestAService diTestAService,
            IDiTestBService diTestBService
            )
        {
            _diTestAService = diTestAService;
            _diTestBService = diTestBService;
        }

        [HttpGet]
        public void Get()
        {
            Console.WriteLine("...");
            _diTestAService.SetAge(18);
            Console.WriteLine("...");
            _diTestBService.Test();
            Console.WriteLine("...");
        }
    }
}
