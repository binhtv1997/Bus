using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        GiatDoDbContext Init();
    }
}
