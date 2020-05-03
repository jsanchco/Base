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

        public User GetById(int id)
        {
            return _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public User Add(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var toRemove = _context.Users.Find(id);
            _context.Users.Remove(toRemove);
            _context.SaveChanges();
        }
    }
}
