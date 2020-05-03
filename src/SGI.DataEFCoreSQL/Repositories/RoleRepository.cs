namespace SGI.DataEFCoreSQL.Repositories
{
    #region Using

    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using SGI.Domain.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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

        public Role GetById(int id)
        {
            return _context.Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Role> GetAll()
        {
            return _context.Roles
                .Include(x => x.Users);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransactionResult<Role>> AddAsync(Role newRole)
        {
            _context.Roles.Add(newRole);
            return new TransactionResult<Role> { Item = newRole, Result = await _context.SaveChangesAsync() > 0 };
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var toRemove = _context.Roles.Find(id);
            _context.Roles.Remove(toRemove);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
