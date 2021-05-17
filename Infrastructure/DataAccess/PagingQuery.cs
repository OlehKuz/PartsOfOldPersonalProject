using Infrastructure.Interfaces;

namespace Infrastructure.DataAccess
{
    public class PagingQuery:IPaging
    {
        public PagingQuery() { }
        public PagingQuery(int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}