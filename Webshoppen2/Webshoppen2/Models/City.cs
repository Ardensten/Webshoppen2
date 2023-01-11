using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class City
    {
        public City()
        {
            Customers = new HashSet<Customer>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
