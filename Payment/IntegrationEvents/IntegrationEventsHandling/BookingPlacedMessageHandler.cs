using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.Helpers;
using Common.Messages;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain;
using Payment.IntegrationEvents.Events;
using Payment.Persistance;
using Payment.Services;

namespace Payment.IntegrationEvents.IntegrationEventsHandling
{
    public class BookingPlacedMessageHandler:IMessageHandler<BookingPlacedMessage>
    {
        private readonly IMapper _mapper;
        private readonly OrderPaymentDbContext _dbContext;

        // THIS SERVICE IS ALL OBSOLETE, BEFORE I KNEW ANY ARCHITECTURE DESIGN PATTERNS. PROBABLY WOULD CREATE
        // CHAIN OF RESPONSIBILITY INSTEAD OF THIS OrderPaymentIntegrationEventService 
        private readonly OrderPaymentIntegrationEventService _orderPaymentIntegrationEventService;
        public BookingPlacedMessageHandler(OrderPaymentDbContext dbContext, IMapper mapper,OrderPaymentIntegrationEventService orderPaymentIntegrationEventService,IServiceScopeFactory scopeFactory) //: base(scopeFactory,dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderPaymentIntegrationEventService = orderPaymentIntegrationEventService;
        }
        
        public async Task HandleAsync(BookingPlacedMessage @event)
        {
            var orderPost = _mapper.Map<BookingPlacedMessage, OrderPostDto>(@event);
            orderPost.UnpayedOrderExpirationTime = DateTime.UtcNow.AddHours(3).ToUnixTimeStamp();// TODO lookup service which sees expiration for different messages
            var order =_mapper.Map<OrderPostDto, Order>(orderPost);
            await _orderPaymentIntegrationEventService.ExecutePaymentOrderRelatedTransaction<OrderPaymentDbContext>(async () =>
            {
                Console.WriteLine("Order start to add");
                var entity = await _dbContext.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Order added");
            },@event, order);
        }
    }
}