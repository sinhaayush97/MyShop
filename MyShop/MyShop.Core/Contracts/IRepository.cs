using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T:BaseEntity 
    {
        IQueryable<T> Collection();
        void Commit();
        void Create(T t);
        T Find(string ID);
        void Delete(string ID);
        void Update(T t);

    }
}
