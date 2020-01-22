using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SocieteApi.Model;

namespace SocieteApi.DalManager.Repository
{
    public class SocieteRepository 
    {
        private ISession _nhsession;
        public SocieteRepository(NHibernateHelper nHibernateHelper) 
        {
            _nhsession=nHibernateHelper.NhSession;
        }
    
        public IEnumerable<Societe> GetAll()
        {
            return _nhsession.Query<Societe>().ToList();
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