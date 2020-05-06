namespace SGI.DataEFCoreSQL.Repositories
{
    #region Using

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Domain.Helpers;
    using System;
    using SGI.Domain.Models;

    #endregion

    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly EFContextSQL _context;

        public UserRepository(EFContextSQL context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users
                .Include(x => x.Role);
        }

        public IQueryable<User> GetByRoles(IEnumerable<int> ids)
        {
            return _context.Users
                .Where(x => ids.Contains(x.RoleId))
                .Include(x => x.Role);
        }

        public User GetById(int id)
        {
            return _context.Users
                .Include(x => x.Role)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransactionResult<User>> AddAsync(User newUser)
        {
            _context.Users.Add(newUser);
            return new TransactionResult<User> { Item = newUser, Result = await _context.SaveChangesAsync() > 0 };
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var toRemove = _context.Users.Find(id);
            _context.Users.Remove(toRemove);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
