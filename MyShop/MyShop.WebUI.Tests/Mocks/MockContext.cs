using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T:BaseEntity
    {
        List<T> items;

        public MockContext()
        {
            items = new List<T>();
        }

        public void Commit()
        {
            return;
        }
        public void Create(T t)
        {
            items.Add(t);
        }

        public T Find(string Id)
        {
            T t = items.Find(x => x.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception( " does not contain the specified item.");
            }
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(x => x.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception( " does not contain the specified item.");
            }
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(x => x.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(" does not contain the specified item.");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
    }
}
