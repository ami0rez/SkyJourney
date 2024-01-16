using Amirez.Infrastructure.Data;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Customers
{
    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
