using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItems()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();

            HttpContextBase httpContext = new MockHttpContext();
            IBasketService basketService = new BasketService(baskets,products);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService,orderService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //controller.AddToBasket("1");
            basketService.AddToBasket(httpContext, "1");
            Basket basket = baskets.Collection().FirstOrDefault();
            
            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1,basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.FirstOrDefault().ProductID);
        }

        [TestMethod]
        public void CanGetSummaryViewModel() {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();

            products.Create(new Product() { Id = "1",Price = 10.00m});
            products.Create(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductID = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductID = "2", Quantity = 1 });
            baskets.Create(basket);

            IBasketService basketService = new BasketService(baskets, products);
            IOrderService orderService = new OrderService(orders);
            var Controller = new BasketController(basketService,orderService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            Controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), Controller);
            var result = Controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketPrice);
            
        }

        [TestMethod]
        public void CanCheckOutAndCreateOrder()
        {
            IRepository<Product> products = new MockContext<Product>();
            products.Create(new Product() {Id = "1",Price = 10.00m });
            products.Create(new Product() { Id = "2", Price = 5.00m });

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem { ProductID = "1" , Quantity = 2,BasketID = basket.Id});
            basket.BasketItems.Add(new BasketItem { ProductID = "2", Quantity = 1, BasketID = basket.Id });
            baskets.Create(basket);
            IBasketService basketService = new BasketService(baskets, products);
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);

            var controller = new BasketController(basketService, orderService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id});
            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(),controller);

            //Act
            Order order = new Order();
            controller.CheckOut(order);

            //Assert
            Assert.AreEqual(2, order.orderItems.Count());
            Assert.AreEqual(0, basket.BasketItems.Count());

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.orderItems.Count());
        }
    }
}
