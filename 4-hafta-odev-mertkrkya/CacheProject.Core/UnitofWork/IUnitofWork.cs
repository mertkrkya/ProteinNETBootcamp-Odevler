using System.Threading.Tasks;

namespace CacheProject.Core.UnitofWork
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        void Commit();
    }
}