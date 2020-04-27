namespace SGI.Helpers.DataEFCoreSQL
{
    #region Using

    using Microsoft.EntityFrameworkCore;
    using SGI.Domain.Entities;
    using System;

    #endregion

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrador" },
                new Role { Id = 2, Name = "Trabajador" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Jesús",
                    Surname = "Sánchez",
                    Birthdate = new DateTime(1972, 8, 1),
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Name = "Rubén",
                    Surname = "Morales",
                    Birthdate = new DateTime(1971, 6, 25),
                    RoleId = 1
                },
                new User
                {
                    Id = 3,
                    Name = "Ariadna",
                    Surname = "Pérez",
                    Birthdate = new DateTime(1990, 2, 23),
                    RoleId = 2
                },
                new User
                {
                    Id = 4,
                    Name = "Daniela",
                    Surname = "Aceituno",
                    Birthdate = new DateTime(2008, 8, 10),
                    RoleId = 2
                },
                new User
                {
                    Id = 5,
                    Name = "Pedro",
                    Surname = "González",
                    Birthdate = new DateTime(2000, 12, 24),
                    RoleId = 1
                }
            );
        }
    }
}
