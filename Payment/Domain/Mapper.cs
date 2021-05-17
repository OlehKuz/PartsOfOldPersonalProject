using AutoMapper;
using Infrastructure.Extensions;
using Payment.IntegrationEvents.Events;
using Payment.Services.DeploymentPlans;

namespace Payment.Domain
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            var utcDateTimeToUnixTimeStampConverter = new UtcDateTimeToUnixTimeStampConverter();
            var unixToUtcDateTimeConverter = new UnixToUtcDateTimeConverter();
            CreateMap<BookingPlacedMessage, OrderPostDto>().ForMember(od => od.ClientsId, opt=>opt.MapFrom(src=>src.ClientsId ?? default));
            CreateMap<OrderPostDto, Order>().ForMember(dst=>dst.EndHour, opt=>opt.ConvertUsing(unixToUtcDateTimeConverter))
                .ForMember(dst=>dst.StartHour, opt=>opt.ConvertUsing(unixToUtcDateTimeConverter))
                .ForMember(dst=>dst.UnpayedOrderExpirationTime, opt=>opt.ConvertUsing(unixToUtcDateTimeConverter));
            CreateMap<Stage, StageLog>().ForMember(st => st.FunctionToCall,
                opt => opt.MapFrom(src => nameof(src.FunctionToCall.ToString)));
            CreateMap<Stage, StageLog>().ForMember(st => st.FunctionToRestore,
                opt => opt.MapFrom(src => nameof(src.FunctionToRestore.ToString)));
        }
    }
}