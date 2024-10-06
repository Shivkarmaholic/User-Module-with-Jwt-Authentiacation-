using KarmaBook.Models;

namespace KarmaBook.Repositories.Interfaces
{
    public interface IUserRepository
    {
       
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetById(int id);
        public Task<List<User>> GetAll();


    }
}
