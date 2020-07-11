using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.DataAccess.Common
{
    public interface IBaseIdentityRepository<T> where T : class
    {
        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Get All list Object
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Function use to Execute SQL Command Line
        /// </summary>
        /// <typeparam name="TEntity">Object is target</typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecuteSql<TEntity>(string sql) where TEntity : class;

        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        /// <summary>
        /// Function use in the case Query have condition
        /// </summary>
        /// <param name="filter">Condition of query</param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Function use to Update Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// Function use to Insert Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// Inserts the multiple entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        List<T> InsertMulti(List<T> entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="id">Id is identity</param>
        /// <returns></returns>
        bool Delete(dynamic id);

        /// <summary>
        /// Deletes the mullti.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool DeleteMulti(List<T> entity);

        T Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        List<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    }
    public class BaseIdentityRepository<T> : IBaseIdentityRepository<T> where T : class
    {
        public DbSet<T> Dbset;
        protected readonly ARPrjContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public BaseIdentityRepository(ARPrjContext context)
        {
            DbContext = context;
            DbContext.Configuration.ProxyCreationEnabled = false;
            Dbset = context.Set<T>();
        }

        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return Dbset.Find(id);
        }

        /// <summary>
        /// Get All list Object
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQueryable()
        {
            return Dbset.AsQueryable();
        }

        /// <summary>
        /// Function use to Execute SQL Command Line
        /// </summary>
        /// <typeparam name="TEntity">Object is target</typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> ExecuteSql<TEntity>(string sql) where TEntity : class
        {
            return DbContext.Database.SqlQuery<TEntity>(sql);
        }

        public int ExecuteSql(string sql)
        {
            return DbContext.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// Function use in the case Query have condition
        /// </summary>
        /// <param name="filter">Condition of query</param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return Dbset.Where(filter);
        }

        /// <summary>
        /// Function use to Update Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        public T Update(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Dbset.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// Function use to Insert Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        public T Insert(T entity)
        {
            Dbset.Add(entity);

            return entity;
        }

        public List<T> InsertMulti(List<T> entity)
        {
            Dbset.AddRange(entity);
            return entity;
        }

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            try
            {
                var entry = DbContext.Entry(entity);
                entry.State = EntityState.Deleted;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public virtual bool Delete(dynamic id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return false;
            }

            Delete(entity);
            return true;
        }

        public bool DeleteMulti(List<T> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    Delete(item);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public T Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Dbset.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(expression);
            return query.FirstOrDefault();
        }
        public List<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Dbset.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query.ToList();
        }
    }
}

