using TwitterBroadcastSystemModel.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace TwitterBroadcastSystemModel
{
    public class TBSEntities : TBSContext
    {
        private new TBSDatabase Database;
        int _DontSave = 0;
        // This is fine as TBSEntities should be unique per thread & purged after each request, according to Global.asax.cs setup.
        private readonly Dictionary<string, DbPropertyValues> _OriginalValuesCache = new Dictionary<string, DbPropertyValues>();

        public TBSEntities() : base()
        {
            Database = new TBSDatabase(base.Database);
        }

        protected override void Dispose(bool disposing)
        {
            if (Database.Connection.State != ConnectionState.Closed)
            {
                Database.Connection.Close();
            }
            base.Dispose(disposing);
        }

        public void LogException(Exception ex, string location)
        {
            //WriteLog(Enums.LogLevel.Error, BaseApiController.GetLoginGUID(), Enums.LogCode.Entities, location, ex.Message, ex.StackTrace);
        }

        public void QueueSaveChanges(Action work)
        {
            _DontSave++;
            work.Invoke();
            _DontSave--;
        }

        public override int SaveChanges()
        {
            try
            {
                if (_DontSave != 0)
                {
                    return 0;
                }
                var changedEntities =
                    ChangeTracker.Entries()
                        .Where(t => t.State != EntityState.Unchanged && t.State != EntityState.Detached)
                        .ToList();

                if (changedEntities.Any() == false)
                {
                    return base.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Error occured, but still try to save what we can
                //LogException(ex, "SaveChanges");
            }
            finally
            {
                // Make sure to purge this in case we call save multiple times in a single request.
                _OriginalValuesCache.Clear();
            }

            return base.SaveChanges();
        }

        private DbPropertyValues RetrieveEntity(Guid idGUID, DbEntityEntry entry)
        {
            var id = $"{idGUID}-{entry.Entity.GetType()}";

            if (_OriginalValuesCache.ContainsKey(id) == false)
            {
                _OriginalValuesCache[id] = entry.GetDatabaseValues();
            }

            return _OriginalValuesCache[id];
        }

        private DbEntityEntry<T> EntryAndAttach<T>(T entity, string idProperty) where T : class
        {
            // Check if it exists in Local first
            var localEntity = Set<T>()
                .Local.FirstOrDefault(
                    e => e.GetPropertyValue<Guid>(idProperty) == entity.GetPropertyValue<Guid>(idProperty));

            if (localEntity != null)
            {
                return Entry(localEntity);
            }

            var entry = Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                Set<T>().Attach(entity);
            }

            return entry;
        }
    }

    public static class DbSetExtensions
    {
        // Make it easy to pull the local entity if it exists (might be required for entities that are 'Added')
        public static IQueryable<T> GetLocalOrAsNoTracking<T>(this DbSet<T> dbSet, Func<T, bool> predicate) where T : class
        {
            var local = dbSet.Local.Where(predicate).AsQueryable();

            return local.Any() ? local : dbSet.AsNoTracking().Where(predicate).AsQueryable();
        }
    }

    public class TBSDatabase
    {
        private Database _Database;
        public TBSConnection Connection { get; set; }
        public DbContextTransaction CurrentTransaction => _Database.CurrentTransaction;

        public TBSDatabase(Database db)
        {
            _Database = db;
            Connection = new TBSConnection(_Database);
        }

        public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return _Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql)
        {
            return _Database.ExecuteSqlCommand(sql);
        }

        public int ExecuteSqlCommand(string sql, object[] parameters)
        {
            return _Database.ExecuteSqlCommand(sql, parameters);
        }
        //More methods may need to be implemented here.  I only implemented the ones we were currently using.

    }

    public class TBSConnection : DbConnection
    {
        private Database _Database;

        public override string ConnectionString
        {
            get
            {
                return _Database.Connection.ConnectionString;
            }
            set
            {
                _Database.Connection.ConnectionString = value;
            }
        }

        public override string Database => _Database.Connection.Database;

        public override string DataSource => _Database.Connection.DataSource;

        public override string ServerVersion => _Database.Connection.ServerVersion;

        public override ConnectionState State => _Database.Connection.State;

        public TBSConnection(Database db)
        {
            _Database = db;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _Database.Connection.BeginTransaction(isolationLevel);
        }

        public override void Close()
        {
            _Database.Connection.Close();
        }

        public override void ChangeDatabase(string databaseName)
        {
            _Database.Connection.ChangeDatabase(databaseName);
        }

        protected override DbCommand CreateDbCommand()
        {
            //Sinse the EF timeout does not apply to the ADO timeout and there is no way to set the 
            //ADO timeout across the board out of the box, this class will allow the EF and ADO 
            //timeouts to be linked and be applied everywhere.
            DbCommand command = _Database.Connection.CreateCommand();
            command.CommandTimeout = _Database.CommandTimeout ?? 0;
            return command;
        }

        public override void Open()
        {
            _Database.Connection.Open();
        }
    }
}
