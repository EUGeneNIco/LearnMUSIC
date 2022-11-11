using System.Threading.Tasks;

namespace LearnMUSIC.Interface.WebAPI.Auth
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);

        Task<bool> ValidateTokenAsync(string token);
    }
}
