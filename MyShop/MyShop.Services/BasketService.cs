using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Basket> basketContext;
        IRepository<Product> productContext;
        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(IRepository<Basket> BasketContext, IRepository<Product> ProductContext)
        {
            basketContext = BasketContext;
            productContext = ProductContext;
        }
        private Basket GetBasket(HttpContextBase httpContext , bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket = null;
            if(cookie != null)
            {
                string BasketID = cookie.Value;
                if(!string.IsNullOrEmpty(BasketID))
                {
                    basket = basketContext.Find(BasketID);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Create(basket);
            basketContext.Commit();
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(2);
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext,string productID)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == productID);
            if(item != null)
            {
                item.Quantity++;
            }
            else
            {
                item = new BasketItem()
                {
                    BasketID = basket.Id,
                    Quantity = 1,
                    ProductID = productID
                };
                basket.BasketItems.Add(item);
            }
            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext,string productID)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == productID);
            if(item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GeteBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            if(basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.Collection() on b.ProductID equals p.Id
                               select new BasketItemViewModel
                               {
                                   ID = b.Id,
                                   Quantity = b.Quantity,
                                   Price = p.Price,
                                   ProductName = p.Name,
                                   Image = p.Image
                               }
                               ).ToList();
                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel objBasketSummaryViewModel = new BasketSummaryViewModel();
            if (basket != null)
            {
                int? Count = (from b in basket.BasketItems
                             select b.Quantity
                             ).Sum();
                decimal? Price = (from b in basket.BasketItems
                                  join p in productContext.Collection() on b.ProductID equals p.Id
                                  select b.Quantity * p.Price
                                  ).Sum();
                objBasketSummaryViewModel.BasketCount = Count ?? 0;
                objBasketSummaryViewModel.BasketPrice = Price ?? Decimal.Zero;
                return objBasketSummaryViewModel;
            }
            else
            {
                return objBasketSummaryViewModel;
            }
        }

        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }
    }
}
