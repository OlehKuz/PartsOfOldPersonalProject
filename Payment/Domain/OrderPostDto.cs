using System;

namespace Payment.Domain
{
    public class OrderPostDto
    {
         public long ClientsId { get; set; }
         public long StartHour { get; set; }
         public long EndHour { get; set; }
         public long ServicesId { get; set; }
         public long UnpayedOrderExpirationTime { get; set; }
    }
}