using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Infrastructure.DataAccess
{
    public class PagingResponse<T>
    {
        public IEnumerable<T> Elements { get; }
        public int TotalAvailable { get; }
        public PagingResponse(IEnumerable<T> elements, int totalAvailable)
        {
            Elements = elements;
            TotalAvailable=totalAvailable;
        }
    }
}