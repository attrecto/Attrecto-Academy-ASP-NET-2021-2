using Academy_2022.Models;

namespace Academy_2022.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
