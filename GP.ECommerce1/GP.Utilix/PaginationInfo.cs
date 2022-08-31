namespace GP.Utilix;

public class PaginationInfo
{
    public int PageIndex { get; private set; }
    
    public int TotalPages { get; private set; }
    
    public int PageSize { get; private set; }
    
    public int TotalCount { get; private set; }
    
    public bool HasPrevious => PageIndex > 0;
    
    public bool HasNext => PageIndex < TotalPages - 1;
}