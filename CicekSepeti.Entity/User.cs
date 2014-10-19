using System.Collections.Generic;
using CicekSepeti.Entity.Base;

namespace CicekSepeti.Entity
{
    public sealed class User : EntityBase
    {
        public User()
        {
            Activities = new List<Activity>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<Activity> Activities { get; set; } 
    }
}
