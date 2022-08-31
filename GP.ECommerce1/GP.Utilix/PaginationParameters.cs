namespace GP.Utilix;

/// <summary>
/// A class representing the pagination parameters.
/// </summary>
public class PaginationParameters
{
    public int PageIndex { get; set; } = 0;
    
    public int PageSize { get; set; } = 50;

    public static PaginationParameters DefaultMaxParameters = new PaginationParameters
    {
        PageIndex = 0,
        PageSize = 50
    };
}