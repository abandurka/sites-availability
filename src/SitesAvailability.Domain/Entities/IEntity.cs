using System;
using System.Collections.Generic;
using System.Text;

namespace SitesAvailability.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
