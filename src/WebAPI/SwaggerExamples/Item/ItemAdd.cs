using Contracts.Responses;
using Swashbuckle.Examples;

namespace WebAPI.SwaggerExamples.Item;

/// <summary>
/// itemas addddddd
/// </summary>
public class ItemAdd : IExamplesProvider
{
    /// <summary>
    /// pavyzdys
    /// </summary>
    /// <returns></returns>
    public object GetExamples()
    {
        return new ItemAddResponse()
        {
            Id = new Guid()
        };
    }
}
