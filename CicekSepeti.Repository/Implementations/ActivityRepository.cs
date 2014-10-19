using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekSepeti.Data;
using CicekSepeti.Data.Contracts;
using CicekSepeti.Entity;
using CicekSepeti.Repository.Contracts;

namespace CicekSepeti.Repository.Implementations
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            
        }
    }
}
