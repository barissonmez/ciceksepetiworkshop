using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekSepeti.Data.Contracts;

namespace CicekSepeti.Data.Implementations
{
    public class DataContextFactory : IDisposable, IDataContextFactory
    {
        private static CicekSepetiDbContext _dbContext;

        public CicekSepetiDbContext Get()
        {
            return _dbContext ?? (_dbContext = new CicekSepetiDbContext());
        }

        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        
    }
}
