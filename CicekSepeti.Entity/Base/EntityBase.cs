using System;

namespace CicekSepeti.Entity.Base
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
