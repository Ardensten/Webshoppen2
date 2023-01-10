using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class OrderHistory
    {
        public int Id { get; set; }
        public int CheckoutCartOrderId { get; set; }
        public int? ShippingInfoId { get; set;}
    }
}
