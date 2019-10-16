using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IServiceRepository : IRepository<Services>
    {

    }
    public class ServiceRepository : RepositoryBase<Services>, IServiceRepository
    {
        public ServiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
