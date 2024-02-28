using Contracts.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace WebAPI.SwaggerExamples.Item;

/// <summary>
/// example
/// </summary>
public class ItemAddResponseExample : IExamplesProvider<AddResponse>
{
    /// <summary>
    /// example
    /// </summary>
    /// <returns></returns>
    public AddResponse GetExamples()
    {
        return new AddResponse()
        {
            Id = Guid.Parse("51427c65-fb49-42be-a651-a0a1dee84931")
        };
    }
}
