using System.Collections.Generic;
using System.Linq;
using NHibernate;
using ApplicationBusiness.Model;
using System;
using ApplicationBusiness.IDalManager.IRepositories;

namespace ApplicationBusiness.DalManager.Repository
{
    public class SocieteRepositoryNH : ISocieteRepository
    {
        private ISession _nhsession;
        public SocieteRepositoryNH(ISession nhsession) 
        {
            _nhsession= nhsession;
        }
    
        public IEnumerable<Societe> GetAll()
        {
            return _nhsession.Query<Societe>().ToList();
        }

        public Societe GetById(Guid id)
        {
            return _nhsession.Get<Societe>(id);
        }
        public Societe Save(Societe soc)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                soc.Id=(Guid)_nhsession.Save(soc);
                tx.Commit();
            }
            
            return soc;
        }
        public Societe Update(Societe soc)
        {
            using (var tx = _nhsession.BeginTransaction()) {
                _nhsession.Update(soc);
                tx.Commit();
            }
            
            return soc;
        }
    }
}