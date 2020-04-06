using MyShop.Core.Contracts;
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
        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
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
    }
}