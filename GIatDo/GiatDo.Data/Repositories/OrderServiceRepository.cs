using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IOrderServiceRepository : IRepository<OrderService>
    {

    }
    public class OrderServiceRepository : RepositoryBase<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
