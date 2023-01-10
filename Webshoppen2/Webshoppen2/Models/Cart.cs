using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? AmountofUnits { get; set; }
        public int? CustomerId { get; set; }
        public float? OrderId { get; set; }
    }
}
