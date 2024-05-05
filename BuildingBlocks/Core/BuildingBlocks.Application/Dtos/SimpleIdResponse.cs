namespace BuildingBlocks.Application.Dtos;

public class SimpleIdResponse<T>
{
    public T Id { get; set; }
    
    public SimpleIdResponse(T id)
    {
        Id = id;
    }

    public static SimpleIdResponse<T> Create(T id)
    {
        return new SimpleIdResponse<T>(id);
    }
}