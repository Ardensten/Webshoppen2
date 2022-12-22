using System;
using System.Collections.Generic;

namespace Webshoppen2.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public long? SocialSecurityNumber { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? CityId { get; set; }
        public string? Adress { get; set; }
        public int? OrderHistoryId { get; set; }
        public int? PaymentInfoId { get; set; }

        public virtual City? City { get; set; }
    }
}
