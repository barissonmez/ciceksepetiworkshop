using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Data.Contracts
{
    public interface IDataContextFactory : IDisposable
    {
        CicekSepetiDbContext Get();
    }
}
