using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategoryList;

        public ProductCategoryRepository()
        {
            productCategoryList = cache["productCategoryList"] as List<ProductCategory>;
            if(productCategoryList == null)
            {
                productCategoryList = new List<ProductCategory>();
            }
        }
         
        public void Create(ProductCategory productCategory)
        {
            if(productCategory != null)
            {
                productCategoryList.Add(productCategory);
            }
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategoryList.Find(p => p.Id == productCategory.Id);
            if(productCategoryToUpdate != null)
            {
                productCategoryToUpdate.Category = productCategory.Category;
            }
            else
            {
                throw new Exception("Product Category Not Found.");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategoryList.Find(p => p.Id == Id);
            if(productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Does Not Exist.");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategoryList.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategory = productCategoryList.Find(p => p.Id == Id);
            if(productCategory != null)
            {
                productCategoryList.Remove(productCategory);
            }
            else
            {
                throw new Exception("Product Category Does Not Exist.");
            }
        }

        public void Commit()
        {
            cache["productCategoryList"] = productCategoryList;
        }
    }
}
