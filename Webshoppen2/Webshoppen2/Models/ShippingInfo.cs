using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class ShippingInfo
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? ParcelServiceName { get; set; }
    }
}
