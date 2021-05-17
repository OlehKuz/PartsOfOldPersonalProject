namespace Infrastructure.Interfaces
{
    public interface IPaging
    {
        int? PageSize { get; set; }
        int? PageIndex { get; set; }
    }
}