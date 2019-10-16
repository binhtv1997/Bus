using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {

    }
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
