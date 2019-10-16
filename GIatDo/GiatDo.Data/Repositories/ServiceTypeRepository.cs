using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {

    }

    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
