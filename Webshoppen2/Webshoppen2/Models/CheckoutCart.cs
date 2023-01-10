using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshoppen2.Models
{
    internal class CheckoutCart
    {
        public int Id { get; set; }
        public float OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? AmountofUnits { get; set; }
        public int? CustomerId { get; set; }
    }
}
