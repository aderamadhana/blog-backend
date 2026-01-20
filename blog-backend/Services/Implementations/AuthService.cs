using blog_backend.Domain.Entities;
using blog_backend.DTOs.Auth;
using blog_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace blog_backend.Services.Implementations;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }   

        var user = new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var msg = string.Join(" | ", result.Errors.Select(e => e.Description));
            throw new Exception(msg);
        }

        return GenerateJwt(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new Exception("Invalid email or password.");
        }
        return GenerateJwt(user);
    }

    private AuthResponse GenerateJwt(ApplicationUser user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection["Key"];
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var expiryMinutes = _configuration.GetValue<int?>("Jwt:ExpiryInMinutes") ?? 60;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Konfigurasi Jwt:Key belum diset.");
        if (string.IsNullOrWhiteSpace(issuer))
            throw new Exception("Konfigurasi Jwt:Issuer belum diset.");
        if (string.IsNullOrWhiteSpace(audience))
            throw new Exception("Konfigurasi Jwt:Audience belum diset.");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Email ?? "")
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires
        };
    }
}
