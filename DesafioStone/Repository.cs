using DesafioStone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DesafioStone
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> entity;
        protected DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            this.entity = context != null ? _context.Set<T>() : throw new ArgumentNullException("entities");
        }

        public T Add(T entity) // Addtransaction + CreatAccount
        {
            this.entity.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Get(params object[] values)
        {
            return this.entity.Find(values); // o find espera que passe para ele uma lista de objetos de parametro
        }

        public IQueryable<T> GetAll()
        {
            return Queryable.Cast<T>(this.entity);
        }

        public void Remove(T entity)
        {
            this.entity.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.entity.Update(entity);
            _context.SaveChanges();
        }
    }
}