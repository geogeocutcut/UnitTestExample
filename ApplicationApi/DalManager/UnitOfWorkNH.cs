using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ApplicationApi.DalManager.Mapping;

namespace ApplicationApi.DalManager
{
    public class UnitOfWorkNH 
    {
        private readonly object _lockObject = new object();
        private static ISessionFactory _sessionFactory;

        private ISession _nhsession;
        public ISession NhSession
        {
            get
            {
                if (_nhsession == null)
                {
                        _nhsession = _sessionFactory
                        .WithOptions().OpenSession();
                    
                }
                return _nhsession;
            }
        }
        
        public UnitOfWorkNH()
        {
            if (_sessionFactory == null)
            {
                CreateSessionFactory();
            }
        }
    
        private void CreateSessionFactory()
        {
            lock (_lockObject)
            {
                if (_sessionFactory == null)
                {
                    var cfg = new NHibernate.Cfg.Configuration();
                    cfg.Configure("hibernate.cfg.xml");
                    var fluentConfiguration = Fluently.Configure(cfg)
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SocieteMap>());
                    
                    _sessionFactory = fluentConfiguration.BuildSessionFactory();
                }
            }
        }

        public void Dispose()
        {
            if (NhSession.Transaction.IsActive)
                NhSession.Transaction.Rollback();
        }
    }
}