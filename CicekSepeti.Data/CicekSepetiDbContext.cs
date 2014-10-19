using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using CicekSepeti.Data.Initializer;
using CicekSepeti.Data.Mappings;
using CicekSepeti.Entity;

namespace CicekSepeti.Data
{
    public class CicekSepetiDbContext : DbContext
    {
        public CicekSepetiDbContext() : base("CicekSepetiDbContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Database.SetInitializer(new SeedInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();

            ConfigureMappings(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new ActivityMapping());
        }

        public bool Commit()
        {
            try
            {
                return base.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName,
                                      validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                //TODO Log This Error

                return false;
            }
        }


    }
}
