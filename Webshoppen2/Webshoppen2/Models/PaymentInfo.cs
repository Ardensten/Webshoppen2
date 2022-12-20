using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class PaymentInfo
    {
        public int Id { get; set; }
        public string? Method { get; set; }
        public int? CustomerId { get; set; }
        public int? CardNumber { get; set; }
    }
}
