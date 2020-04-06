﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketPrice { get; set; }
        public BasketSummaryViewModel()
        {
            BasketCount = 0;
            BasketPrice = Decimal.Zero;
        }
        public BasketSummaryViewModel(int BasketCount,decimal BasketPrice)
        {
            this.BasketCount = BasketCount;
            this.BasketPrice = BasketPrice;
        }
    }
}
