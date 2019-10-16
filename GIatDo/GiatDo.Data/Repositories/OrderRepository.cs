using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order> { }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
