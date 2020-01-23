using System.Collections.Generic;
using System.Linq;
using NHibernate;
using ApplicationApi.Model;
using System;

namespace ApplicationApi.DalManager.Repository
{
    public class SocieteRepositoryNH 
    {
        private ISession _nhsession;
        public SocieteRepositoryNH(UnitOfWorkNH uow) 
        {
            _nhsession=uow.NhSession;
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
                _nhsession.Save(soc);
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