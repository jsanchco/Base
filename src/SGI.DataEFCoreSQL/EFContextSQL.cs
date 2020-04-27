namespace SGI.DataEFCoreSQL
{
    #region Using

    using System.Threading;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using SGI.Helpers.DataEFCoreSQL;

    #endregion

    public class EFContextSQL : DbContext
    {
        #region Members

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public static long InstanceCount;

        #endregion

        public EFContextSQL(DbContextOptions options) : base(options) => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
