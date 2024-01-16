using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Customers
{
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
    }
}
