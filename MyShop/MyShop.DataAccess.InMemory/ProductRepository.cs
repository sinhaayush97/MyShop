using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> productList;

        public ProductRepository()
        {
            productList = cache["productList"] as List<Product>;
            if(productList == null)
            {
                productList = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["productList"] = productList;
        }

        public void Insert(Product p)
        {
            productList.Add(p);
        }

        public void Update(Product p)
        {
            Product productToUpdate = productList.Find(x => x.Id == p.Id);
            if(productToUpdate != null)
            {
                productToUpdate = p;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }

        public Product Find(string Id)
        {
            Product product = productList.Find(p => p.Id == Id);
            if(product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }

        public IQueryable<Product> Collection()
        {
            return productList.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = productList.Find(p => p.Id == Id);
            if(productToDelete != null)
            {
                productList.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Does Not Exist.");
            }
        }
    }
}
