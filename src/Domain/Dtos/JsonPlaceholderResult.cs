namespace Domain.Dto;

public class JsonPlaceholderResult<T>
{
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
}

public class JsonPlaceholderResult
{
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
}
