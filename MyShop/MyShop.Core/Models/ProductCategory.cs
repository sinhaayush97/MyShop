using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        //public string Id{ get; set; } //AS: Not required anymore as the base class is implementing it.
        public string Category { get; set; }

        //AS: Not required anymore as the base class is implementing it.
        //public ProductCategory()
        //{
        //    Id = Guid.NewGuid().ToString();
        //}
    }
}
