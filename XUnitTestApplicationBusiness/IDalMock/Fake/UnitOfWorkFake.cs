using ApplicationBusiness.IDalManager;
using ApplicationBusiness.IDalManager.IRepositories;
using System;
using System.Collections.Generic;

namespace XUnitTestApplicationBusiness.IDalMock.Fake
{
    public class UnitOfWorkFake : IUnitOfWork
    {

        private Dictionary<object, object> _repositories;
        public UnitOfWorkFake()
        {
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
                if (typeof(TRepository) == typeof(ISocieteRepository))
                {
                    repo = new SocieteRepositoryFake();
                    _repositories[typeof(TRepository)] = repo;
                }
            }
            return (TRepository)repo;

        }
    }
}
