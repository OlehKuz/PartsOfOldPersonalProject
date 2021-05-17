using System;
using Infrastructure.Interfaces;

namespace Payment.Domain
{
    //here should be logic that when user chooses some service and time and books place for it we receive order to this microservice. If order requires master confirmation we send it to master microservice for confirmation.
    //otherwise we save expiry of order in orderstate db and our supervisor checks if order is payed untill order expiry, if not cancels that order and user sees free spot for that order time and master and service.
    // when user wants to see if we have a free spot for some service, out api gateway sends rest http request to scheduling service and joins with response from ordering service (free spots are those that we dont have orders for that service
    // for that time) 
    public  class Order:IEntity<long>
    {
        public long Id { get; set; }
        public long ClientsId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public long ServicesId { get; set; }
        public bool Customerconfirmed { get; set; }
        public DateTime UnpayedOrderExpirationTime { get; set; }
        public bool Canceled { get; set; }
        public string ReasonDeclined { get; set; }
        public bool Masterconfirmed { get; set; }
        
        //new
        public long? PaymentsId { get; set; }
        public Payment Payment { get; set; }
        // public virtual Service Service { get; set; }
    }
}