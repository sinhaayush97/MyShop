using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderService orderService; 
        public BasketController(IBasketService basketService , IOrderService orderService)
        {
            this.basketService = basketService;
            this.orderService = orderService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var Model = basketService.GeteBasketItems(this.HttpContext);
            return View(Model);
        }

        public ActionResult AddToBasket(string ID)
        {
            basketService.AddToBasket(this.HttpContext, ID);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string basketID)
        {
            basketService.RemoveFromBasket(this.HttpContext, basketID);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var basketItems = basketService.GeteBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            //processing payment.

            order.OrderStatus = "Payment Processed."; 
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou",new { OrderID = order.Id});
        }

        public ActionResult ThankYou(string OrderID)
        {
            ViewBag.OrderID = OrderID;
            return View();
        }
    }
}