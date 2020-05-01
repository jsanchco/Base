namespace SGI.DataEFCoreSQL.Repositories
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;

    #endregion

    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly EFContextSQL _context;

        public RoleRepository(EFContextSQL context)
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

        public bool RoleExists(int id)
        {
            return GetById(id) != null;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles
                .Include(x => x.Users)
                .ToList();
        }

        public Role GetById(int id)
        {
            return _context.Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
        }

        public Role Add(Role newRole)
        {
            _context.Roles.Add(newRole);
            _context.SaveChanges();
            return newRole;
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var toRemove = _context.Roles.Find(id);
            _context.Roles.Remove(toRemove);
            _context.SaveChanges();
        }
    }
}
