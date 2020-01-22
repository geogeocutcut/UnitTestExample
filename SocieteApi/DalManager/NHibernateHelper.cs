using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SocieteApi.DalManager.Mapping;
using System.Data.SQLite;

namespace SocieteApi.DalManager
{
    public class NHibernateHelper 
    {
        private readonly string _connectionString;
        private readonly object _lockObject = new object();
        private static ISessionFactory _sessionFactory;
        private static NHibernate.Cfg.Configuration _configuration;
        private SQLiteConnection _connection;

        const string CONNECTION_STRING = "Data Source=:memory:;BinaryGuid=False";
        private ISession _nhsession;
        public ISession NhSession
        {
            get
            {
                if (_nhsession == null)
                {
                        _nhsession = _sessionFactory
                        .WithOptions()
                        .Connection(GetConnection()).OpenSession();
                    
                }
                return _nhsession;
            }
        }
        private SQLiteConnection GetConnection()
        {
            if (null == _connection)
                BuildConnection();
            return _connection;
        }

        private void BuildConnection()
        {
            _connection = new SQLiteConnection(CONNECTION_STRING);
            _connection.Open();
            BuildSchema();
        }

        private void BuildSchema()
        {
            SchemaExport se= new SchemaExport(_configuration);
            se.Execute(false, true, false, _connection, null);
            SQLiteCommand cmd = new SQLiteCommand(_connection);
            cmd.CommandText = string.Format("PRAGMA foreign_keys = ON");
            cmd.ExecuteNonQuery();
        }   
        public NHibernateHelper(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
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
                    var fluentConfiguration = Fluently.Configure()
                        .Database(
                            //MySQLConfiguration.Standard.ConnectionString(_connectionString)
                        //.ShowSql()
                            SQLiteConfiguration.Standard.InMemory().ShowSql()
                        )
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SocieteMap>())
                        .ExposeConfiguration(config =>
                        {
                            var schemaExport = new SchemaExport(config);
                            _configuration = config;
                            schemaExport.Execute(true, true, false);
                        });
                    
                    _sessionFactory = fluentConfiguration.BuildSessionFactory();
                }
            }
        }
    }
}