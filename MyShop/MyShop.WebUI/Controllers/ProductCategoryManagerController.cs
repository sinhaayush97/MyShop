using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        //ProductCategoryRepository productCategoryRepo;
        IRepository<ProductCategory> productCategoryRepo;
        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            productCategoryRepo = productCategoryContext;
        }
        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productCategoryList = productCategoryRepo.Collection().ToList();
            return View(productCategoryList);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                productCategoryRepo.Create(productCategory);
                productCategoryRepo.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = productCategoryRepo.Find(Id);
            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory,string Id)
        {
            ProductCategory productCategoryToUpdate = productCategoryRepo.Find(Id);
            if(productCategoryToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                productCategoryRepo.Update(productCategory);
                productCategoryRepo.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategory = productCategoryRepo.Find(Id);
            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategory = productCategoryRepo.Find(Id);
            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                productCategoryRepo.Delete(Id);
                productCategoryRepo.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}