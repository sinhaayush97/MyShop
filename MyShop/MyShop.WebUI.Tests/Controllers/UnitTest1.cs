using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new MockContext<ProductCategory>();
            productContext.Create(new Product());
            HomeController controller = new HomeController(productContext, productCategoryContext); 
            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;
            Assert.AreEqual(1, viewModel.Product.Count());
        }
    }
}
