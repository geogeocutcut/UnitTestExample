using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBusiness.IDalManager
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
    }
}
