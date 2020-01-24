using ApplicationBusiness.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBusiness.IDalManager.IRepositories
{
    public interface ISocieteRepository
    {
        IEnumerable<Societe> GetAll();
        Societe GetById(Guid id);
        Societe Save(Societe soc);
        Societe Update(Societe soc);
    }
}
