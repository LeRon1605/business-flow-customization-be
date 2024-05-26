using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Presentation.Authorization;

public class InternalApiAttribute : AuthorizeAttribute
{
    public InternalApiAttribute() : base("InternalApi")
    {
    }
}