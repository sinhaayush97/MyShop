using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        public ActionResult Index()
        {
            List<Product> productList = productContext.Collection().ToList();
            return View(productList);        
        }

        public ActionResult Details(string ID)
        {
            Product product = productContext.Find(ID);
            return View(product);
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