using Interview_Api.DTOs;
using Interview_Api.Models;
using Interview_Api.Repositories.Interfaces;
using Interview_Api.Services.IServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<User?> GetUserAsync(int id) => _repository.GetByIdAsync(id);

    public Task<List<User>> GetAllUsersAsync() => _repository.GetAllAsync();

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

        return await _repository.AddAsync(user);
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return false;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;

        return await _repository.UpdateAsync(user);
    }

    public Task<bool> DeleteUserAsync(int id) => _repository.DeleteAsync(id);
}
