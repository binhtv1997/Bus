using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        GiatDoDbContext dbContext;

        public GiatDoDbContext Init()
        {
            return dbContext ?? (dbContext = new GiatDoDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
