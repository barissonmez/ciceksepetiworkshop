using System;
using CicekSepeti.Entity.Base;

namespace CicekSepeti.Entity
{
    public class Activity : EntityBase
    {
        public DateTime LoginDateTime { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
