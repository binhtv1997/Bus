using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IShipperRepository : IRepository<Shipper>
    {

    }
    public class ShipperRepository : RepositoryBase<Shipper>, IShipperRepository
    {
        public ShipperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
