using System;
using Common.Enums;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Payment.IntegrationEvents.Events;
using RabbitMq;

namespace Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController:ControllerBase
    {
        private IMessageBus _messageBus;

        public OrderingController(IMessageBus messageBus) // TODO remove message bus, it is only for test, bookingplaced message should be published in another project
        {
            _messageBus = messageBus;
        }

        [HttpGet]
        [Route("Publish")]
        public void PublishPlaceOrder()
        {
            var message = new BookingPlacedMessage(){ClientsId = 2, EndHour = DateTime.UtcNow.AddHours(2).ToUnixTimeStamp(),StartHour = DateTime.UtcNow.AddHours(1).ToUnixTimeStamp(),ServicesId = 1};
            var name = typeof(BookingPlacedMessage).ToString();
            _messageBus.Publish(message,$"exc.{name}", RabbitExchangeType.DirectExchange, name);
         
        }
    }
}