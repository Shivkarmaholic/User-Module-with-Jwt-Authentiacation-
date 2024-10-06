using KarmaBook.Context;
using KarmaBook.Models;
using KarmaBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KarmaBook.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            var result = await _context.Users.ToListAsync() ;
            if (result == null)
            {
                return new List<User>();
            }
            return result;
        }

        public async Task<User> GetById(int id)
        {
            if(id != 0)
            {
                var result = await _context.Users.FindAsync(id);
                if (result == null)
                {
                    return new User();
                }
                return result;
            }
            return new User();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (email != null)
            {
                var result = await _context.Users.FirstOrDefaultAsync<User>(x=> x.Email==email);
                if (result != null)
                {
                    return result;
                }                
            }
            return new User();
        }

        

       
    }
}
