using System.Data.Entity.ModelConfiguration;
using CicekSepeti.Entity;

namespace CicekSepeti.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("Users");

            HasKey(user => user.Id);

            Property(user => user.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            Property(user => user.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
            Property(user => user.Email).HasColumnName("Email").IsRequired().HasMaxLength(250);
            Property(user => user.HashedPassword).HasColumnName("HashedPassword").IsRequired().HasMaxLength(250);
            Property(user => user.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasMaxLength(50);
            Property(user => user.CreationDate).HasColumnName("CreationDate").IsRequired().HasColumnType("datetime");
        }
    }
}
