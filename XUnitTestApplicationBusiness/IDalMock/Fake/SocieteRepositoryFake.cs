using ApplicationBusiness.IDalManager.IRepositories;
using ApplicationBusiness.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestApplicationBusiness.IDalMock.Fake
{
    public class SocieteRepositoryFake : ISocieteRepository
    {
        public IEnumerable<Societe> GetAll()
        {
            return null;
        }

        public Societe GetById(Guid id)
        {
            return new Societe {
                           Id=id,
                           Nom="Fake",
                           Siret= "43311590400057"
            };
        }

        public Societe Save(Societe soc)
        {
            return soc;
        }

        public Societe Update(Societe soc)
        {
            return soc;
        }
    }
}
