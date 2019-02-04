using System;
using System.Threading.Tasks;
using SitesAvailability.Domain;
using SitesAvailability.Domain.Entities;
using SitesAvailability.Domain.Interfaces.Repositories;

namespace SitesAvailability.Infrastructure.Repository
{
    public class AvailabilityCheckingRecordRepository: IRepository<AvailabilityCheckingRecord>
    {
        public Task<IEntity> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
