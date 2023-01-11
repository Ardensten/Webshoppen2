using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class OrderHistory
    {
        public int Id { get; set; }
        public float? CheckoutCartOrderId { get; set; }
        public int? ShippingInfoId { get; set;}
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public int? PaymentInfoId { get; set; }
    }
}
