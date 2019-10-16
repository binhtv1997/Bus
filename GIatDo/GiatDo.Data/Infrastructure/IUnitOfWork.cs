using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
