using System;
using AutoMapper;
using Common.Helpers;

namespace Infrastructure.Extensions
{
    public class UtcDateTimeToUnixTimeStampConverter:IValueConverter<DateTime,long>
    {
        public long Convert(DateTime sourceMember, ResolutionContext context) => sourceMember.ToUnixTimeStamp();
    }
}