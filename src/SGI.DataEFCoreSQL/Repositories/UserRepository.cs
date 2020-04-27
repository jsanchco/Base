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

        private bool UserExists(int id)
        {
            return GetById(id) != null;
        }

        public IEnumerable<User> GetAll()
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

        public User Add(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public bool Update(User user)
        {
            if (!UserExists(user.Id))
                return false;

            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!UserExists(id))
                return false;

            var toRemove = _context.Users.Find(id);
            _context.Users.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}
