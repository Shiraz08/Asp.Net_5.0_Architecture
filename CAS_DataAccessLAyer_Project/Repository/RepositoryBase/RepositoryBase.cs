using CAS_DataAccessLayer.Data;
using CAS_DataAccessLAyer_Project.Interface.IRepositoryBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Repository.RepositoryBase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected context _context { get; set; }
        public RepositoryBase(context context)
        {
            this._context = context;
        }
        public IQueryable<T> FindAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
    }
}
