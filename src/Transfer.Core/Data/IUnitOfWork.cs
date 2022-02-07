using System.Threading.Tasks;

namespace Transfer.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
