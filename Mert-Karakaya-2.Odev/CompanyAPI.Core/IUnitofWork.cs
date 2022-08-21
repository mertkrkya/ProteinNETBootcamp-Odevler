using System.Threading.Tasks;

namespace CompanyAPI.Core
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        void Commit();
    }
}