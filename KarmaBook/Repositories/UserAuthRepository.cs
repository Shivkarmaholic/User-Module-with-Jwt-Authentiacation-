using KarmaBook.Context;
using KarmaBook.Extensions;
using KarmaBook.Models;
using KarmaBook.Models.DTOs;
using KarmaBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KarmaBook.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        public UserAuthRepository(AppDbContext context, TokenService tokenService) 
        {
            _context = context;
            _tokenService = tokenService;
        }

        public Task<ResponseResult> Login(LoginDto login)
        {
            ResponseResult result = new ();

            if (login.Email != string.Empty && login.Password != string.Empty)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == login.Email);
                if (existingUser != null)
                {
                    if (existingUser.Password == login.Password)
                    {
                        existingUser.Password =string.Empty;
                        result.Success = true;
                        result.Message = "Login Successful";
                        result.StatusCode = 200;
                        result.Data = new
                        {
                            User = existingUser,
                            Token = _tokenService.Create(existingUser)
                        };
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Incorrect Password";
                        result.StatusCode = 403;
                        result.Data = login.Email;
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "User not Found";
                    result.StatusCode = 401;
                    result.Data = login.Email;
                }
                return Task.FromResult(result);
            }
            else
            {
                result.Success = false;
                result.Message = "Enter correct login details";
                result.StatusCode = 401;
                return Task.FromResult(result);

            }
        }

        public async Task<bool> Register(User user)
        {
            if (user != null)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync<User>(x => x.Email == user.Email);
                var res = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }
    }
}
