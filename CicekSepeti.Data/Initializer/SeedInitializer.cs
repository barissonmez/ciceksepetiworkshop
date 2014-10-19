using CicekSepeti.Entity;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace CicekSepeti.Data.Initializer
{
    public class SeedInitializer : DropCreateDatabaseIfModelChanges<CicekSepetiDbContext>
    {
        protected override void Seed(CicekSepetiDbContext context)
        {

            context.Users.AddOrUpdate(
                              new User()
                                    {
                                        FirstName = "brs",
                                        LastName = "snmz",
                                        Email = "a@b.com",
                                        HashedPassword = "eoMcv+CKIl1A94sBaFRzlAfhNUUPSHFfLRz0p7/0W0Y=",
                                        PasswordSalt = "SXO3NZRVkgWfgWa08P37HGEfhHpvLHkKEyFROw52F2BLdLEbnr"
                                    }
                            );

            context.SaveChanges();
        }
    }
}