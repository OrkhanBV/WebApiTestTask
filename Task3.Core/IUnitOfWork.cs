using System;
using System.Threading.Tasks;
using Task3.Core.Repositories;

namespace Task3.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IMaterialVersionRepository MaterialVersions { get; }
        IMaterialRepository Materials { get; }
        Task<int> CommitAsync();
    }
}