using System.Threading.Tasks;
using Task3.Core;
using Task3.Core.Models;
using Task3.Core.Repositories;
using Task3.DAL.Repositories;

namespace Task3.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private Task3DbContext _context;
        private MaterialVersionRepository _materialVersionRepository;
        private MaterialRepository _materialRepository;

        public UnitOfWork(Task3DbContext context)
        {
            this._context = context;
        }

        public IMaterialVersionRepository MaterialVersions => _materialVersionRepository = _materialVersionRepository 
            ?? new MaterialVersionRepository(_context);

        public IMaterialRepository Materials => _materialRepository = _materialRepository
            ?? new MaterialRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}