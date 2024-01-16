using Microsoft.EntityFrameworkCore;
using Sistema.Data;
using Sistema.Models;
using Sistema.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace Sistema.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransactionSystemDbContext _dbContextUser;
        public UserRepository(TransactionSystemDbContext dbContext)
        {
            _dbContextUser = dbContext;
        }
        public async Task<List<UserModel>> GetAll()
        {
            return await _dbContextUser.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _dbContextUser.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UserModel> AddUser(UserModel user)
        {
            await _dbContextUser.Users.AddAsync(user);

            await _dbContextUser.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel usuario = await GetById(id);

            if (usuario.Equals(null)) throw new Exception($"User not found by id: {id}");

            _dbContextUser.Users.Remove(usuario);

            await _dbContextUser.SaveChangesAsync();

            return true;
        }


        public async Task<UserModel> UpdateUser(UserModel user, int id)
        {

            UserModel usuario = await GetById(id);

            if (usuario.Equals(null)) throw new Exception($"User not found by id: {id}");

            usuario.Name = user.Name;

            _dbContextUser.Users.Update(usuario);

            await _dbContextUser.SaveChangesAsync();

            return usuario;
        }
    }
}
