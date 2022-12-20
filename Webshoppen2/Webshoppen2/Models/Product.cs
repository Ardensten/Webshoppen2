using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? InfoText { get; set; }
        public int? Price { get; set; }
        public int? UnitsInStock { get; set; }
        public bool? ChosenProduct { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
