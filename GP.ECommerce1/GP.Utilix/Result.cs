namespace GP.Utilix;

public class Result
{
    public bool IsSuccess { get; set; }

    public DatabaseActionSummary DatabaseActionSummary { get; set; } = new();

    public string Error { get; set; } = "";
}

public class Result<T> : Result
{
    public PaginationInfo? PaginationInfo { get; set; }
    
    public T Value { get; set; }
}

public class TestingResult : Result
{
    public string ActionName { get; set; } = "";

    public List<int> Millis { get; set; } = new();
}