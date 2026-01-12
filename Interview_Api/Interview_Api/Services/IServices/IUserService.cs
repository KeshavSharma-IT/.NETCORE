using Interview_Api.DTOs;
using Interview_Api.Models;

namespace Interview_Api.Services.IServices
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
