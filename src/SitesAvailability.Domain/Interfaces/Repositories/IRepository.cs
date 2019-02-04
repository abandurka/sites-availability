using System;
using System.Threading.Tasks;
using SitesAvailability.Domain.Entities;

namespace SitesAvailability.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        Task<IEntity> GetAsync(Guid id);
        Task SaveAsync(IEntity entity);
        Task DeleteAsync(Guid id);
    }
}
