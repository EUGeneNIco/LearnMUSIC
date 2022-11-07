namespace LearnMUSIC.Auth
{
    public interface IJwtAuthenticationManager
    {
        Task<string> AuthenticateAsync(string username, string password);

        Task<bool> ValidateTokenAsync(string token);
    }
}
