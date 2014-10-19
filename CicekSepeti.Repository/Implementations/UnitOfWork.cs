using CicekSepeti.Data;
using CicekSepeti.Data.Contracts;
using CicekSepeti.Repository.Contracts;

namespace CicekSepeti.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CicekSepetiDbContext _dbContext;

        public UnitOfWork(IDataContextFactory dbContext)
        {
            _dbContext = _dbContext ?? dbContext.Get();
        }

        public bool Commit()
        {
            return _dbContext.Commit();
        }
    }
}