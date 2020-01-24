using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ApplicationApi.DalManager.Mapping;
using System.Collections.Generic;
using ApplicationApi.DalManager.Repository;

namespace ApplicationApi.DalManager
{
    public class UnitOfWorkNH 
    {

        private Dictionary<object, object> _repositories;
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
            _repositories = new Dictionary<object, object>();
        }

        public TRepository GetRepository<TRepository>()
        {
            object repo = null;
            if (_repositories.ContainsKey(typeof(TRepository)))
            {
                repo = _repositories[typeof(TRepository)];
            }
            else
            {
                if (typeof(TRepository) == typeof(SocieteRepositoryNH))
                {
                    repo = new SocieteRepositoryNH(NhSession);
                    _repositories[typeof(TRepository)] = repo;
                }
            }
            return (TRepository)repo;

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