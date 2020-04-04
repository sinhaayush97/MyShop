using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IRepository<Product> productContext;
        public IRepository<ProductCategory> productCategoriesContext;
        
        public HomeController(IRepository<Product> productContext,IRepository<ProductCategory> productCategoriesContext)
        {
            this.productContext = productContext;
            this.productCategoriesContext = productCategoriesContext;
        }
        public ActionResult Index(string Category=null)
        {
            List<Product> productList;
            List<ProductCategory> productCategoryList = productCategoriesContext.Collection().ToList();
            if(Category == null)
            {
                productList = productContext.Collection().ToList();
            }
            else
            {
                productList = productContext.Collection().Where(item => item.Category == Category).ToList();
            }
            ProductListViewModel objProductListViewModel = new ProductListViewModel();
            objProductListViewModel.Product = productList;
            objProductListViewModel.ProductCategories = productCategoriesContext.Collection().ToList();
            return View(objProductListViewModel);        
        }

        public ActionResult Details(string ID)
        {
            Product product = productContext.Find(ID);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
            return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}