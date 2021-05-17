using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Payment.Domain
{
    public class Payment:IEntity<long>
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentMadeDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}