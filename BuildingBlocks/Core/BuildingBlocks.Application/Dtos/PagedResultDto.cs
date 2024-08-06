namespace BuildingBlocks.Application.Dtos;

public class PagedResultDto<T>
{
    public long Total { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }
    
    public PagedResultDto(long total, int size, IEnumerable<T> data)
    {
        Total = total;
        TotalPages = (int)Math.Ceiling(total * 1.0 / size);
        Data = data;
    }
}