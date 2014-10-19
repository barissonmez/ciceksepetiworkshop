using CicekSepeti.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CicekSepeti.Data.Mappings
{
    public class ActivityMapping : EntityTypeConfiguration<Activity>
    {
        public ActivityMapping()
        {
            ToTable("Activities");

            HasKey(activity => activity.Id);

            Property(activity => activity.LoginDateTime).HasColumnName("LoginDateTime").IsRequired().HasColumnType("datetime");
            Property(activity => activity.CreationDate).HasColumnName("CreationDate").IsRequired().HasColumnType("datetime");

            HasRequired(activity => activity.User).WithMany(user => user.Activities).HasForeignKey(key => key.UserId);
        }
    }
}
