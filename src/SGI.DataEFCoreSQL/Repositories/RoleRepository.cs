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

        private bool RoleExists(int id)
        {
            return GetById(id) != null;
        }

        public List<Role> GetAll()
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

        public bool Update(Role role)
        {
            if (!RoleExists(role.Id))
                return false;

            _context.Roles.Update(role);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!RoleExists(id))
                return false;

            var toRemove = _context.Roles.Find(id);
            _context.Roles.Remove(toRemove);
            _context.SaveChanges();
            return true;

        }
    }
}
