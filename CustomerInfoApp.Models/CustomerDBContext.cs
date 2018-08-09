using CustomerInfoApp.Models.AuditEntries;
using CustomerInfoApp.Models.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CustomerInfoApp.Models
{
    public class CustomerDBContext: DbContext
    {
        public CustomerDBContext(): base("Name=CustomerDBContext")
        {

        }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified));
            try
            {
                foreach (var entry in modifiedEntries)
                {
                    IAuditableEntity entity = entry.Entity as IAuditableEntity;
                    if (entity != null)
                    {                        
                        DateTime now = DateTime.UtcNow;
                        if (entry.State == EntityState.Added)
                        {                            
                            entity.CreatedDate = now;
                            entity.Status = true;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }                        
                        entity.UpdatedDate = now;
                    }
                }

                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                var validationErrors = dbEx.EntityValidationErrors;
                var validationError = validationErrors.SelectMany(x => x.ValidationErrors).ToList();
                throw dbEx;
            }
        }
    }
}
