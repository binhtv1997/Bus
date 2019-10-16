using GiatDo.Data.Infrastructure;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Repositories
{
    public interface ISlotRepository : IRepository<Slot>
    {

    }
    public class SlotRepository : RepositoryBase<Slot>, ISlotRepository
    {
        public SlotRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
