using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.SharedLibirary.CommonResult;
using ECommerce.SharedLibirary.DTO_s.IdentityDTOs;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;

namespace ECommerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        // Role Management
        Task<Result<string>> AssignRoleAsync(AssignRoleDTO assignRoleDTO);

        // Login
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        // Register
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);

        // Generate JWT Token
        Task<string> GenerateJWTToken(AppUser appUser);

        // Check email existence
        Task<bool> CheckEmailAsync(string Email);

        // Get all users (SuperAdmin)
        Task<Result<IEnumerable<UserDTO>>> GetAllUsersAsync();

        // Delete a user by email (SuperAdmin)
        Task<Result<string>> DeleteUserAsync(string email);

        // Revoke a user's refresh token by email (SuperAdmin)
        Task<Result<string>> RevokeRefreshTokenAsync(string email);

        // Get current user address
        Task<Result<AddressDTO>> GetUserAddressAsync(string email);

        Task<Result<AddressDTO>> UpdateUserAddressAsync(string email, AddressDTO NewAddress);

        // Get current user
        Task<Result<UserDTO>> GetCurrentUserAsync(string email, string Token);

        Task<Result<UserDTO>> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);

        // Google OAuth
        Task<Result<UserDTO>> HandleGoogleLoginAsync(string email, string name, string googleId);
    }
}
