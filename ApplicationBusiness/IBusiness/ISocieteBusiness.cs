using ApplicationBusiness.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBusiness.IBusiness
{
    public interface ISocieteBusiness
    {
        IEnumerable<Societe> GetAll();

        Societe Get(Guid id);

        Societe Add(Societe societe);

        Societe Update(Societe societe);
    }
}
