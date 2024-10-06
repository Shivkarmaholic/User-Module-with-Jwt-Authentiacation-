using KarmaBook.Models;
using KarmaBook.Models.DTOs;

namespace KarmaBook.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        public Task<bool> Register(User user);
        public Task<ResponseResult> Login(LoginDto login);
    }
}
