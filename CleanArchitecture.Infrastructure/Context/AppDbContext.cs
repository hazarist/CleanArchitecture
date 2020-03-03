using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CleanArchitecture.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public override int SaveChanges()
        {
            var entityObjects = ChangeTracker.Entries().Where(e => (e.State == EntityState.Modified || e.State == EntityState.Added));
            foreach (var entityObject in entityObjects)
            {
                if ((entityObject.Entity as BaseEntity) != null)
                {
                    if ((entityObject.Entity as BaseEntity).Id > 0)
                    {
                        entityObject.State = EntityState.Modified;
                        (entityObject.Entity as BaseEntity).DateModified = DateTime.UtcNow;
                    }
                    else
                    {
                        (entityObject.Entity as BaseEntity).DateCreated = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChanges();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            var result = this.Database.ExecuteSqlCommand(sql, parameters);
            base.SaveChanges();
            return result;
        }
    }
}
