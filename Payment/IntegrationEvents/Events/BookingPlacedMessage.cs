using System;
using Common.Messages;

namespace Payment.IntegrationEvents.Events
{
    public class BookingPlacedMessage:RabbitMessage
    {
        public long? ClientsId { get; set; }
        public long StartHour { get; set; }
        public long EndHour { get; set; }
        public long ServicesId { get; set; }
    }
}