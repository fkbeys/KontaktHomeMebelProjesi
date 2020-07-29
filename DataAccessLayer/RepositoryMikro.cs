using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RepositoryMikro<T> : RepositoryBaseMikro where T : class
    {
        private DbSet<T> _objectSet;

        public RepositoryMikro()
        {
            _objectSet = contextmikro.Set<T>();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return _objectSet.SqlQuery(query, parameters).ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {           
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return contextmikro.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
