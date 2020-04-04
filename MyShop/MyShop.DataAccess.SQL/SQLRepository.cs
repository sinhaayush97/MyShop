using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext Context;
        internal DbSet<T> dbSet;
        public SQLRepository(DataContext Context)
        {
            this.Context = Context;
            this.dbSet = Context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Create(T t)
        {
            dbSet.Add(t);
        }

        public void Delete(string ID)
        {
            var t = Find(ID);
            if (Context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string ID)
        {
            return dbSet.Find(ID);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            Context.Entry(t).State = EntityState.Modified;
        }
    }
}
