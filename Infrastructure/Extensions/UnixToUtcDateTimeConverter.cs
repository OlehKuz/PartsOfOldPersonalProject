using System;
using AutoMapper;
using Common.Helpers;

namespace Infrastructure.Extensions
{
    public class UnixToUtcDateTimeConverter:IValueConverter<long, DateTime>
    {
        public DateTime Convert(long sourceMember, ResolutionContext context) => sourceMember.ToUtcDateTime();
    }
}