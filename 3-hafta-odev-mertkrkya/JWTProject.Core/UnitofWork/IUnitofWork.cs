using System.Threading.Tasks;

namespace JWTProject.Core.UnitofWork
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        void Commit();
    }
}