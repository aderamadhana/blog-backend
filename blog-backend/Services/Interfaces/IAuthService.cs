namespace blog_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<DTOs.Auth.AuthResponse> RegisterAsync(DTOs.Auth.RegisterDto registerDto);
        Task<DTOs.Auth.AuthResponse> LoginAsync(DTOs.Auth.LoginDto loginDto);
    }
}
