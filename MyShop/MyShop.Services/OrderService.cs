using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> orderContext)
        {
            this.orderContext = orderContext;
        }
        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItem)
        {
            foreach(var item in basketItem)
            {
                baseOrder.orderItems.Add(new OrderItem
                {
                    ProductID= item.ID,
                    Image = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName
                }); 
            }
            orderContext.Create(baseOrder);
            orderContext.Commit();
        }
    }
}
