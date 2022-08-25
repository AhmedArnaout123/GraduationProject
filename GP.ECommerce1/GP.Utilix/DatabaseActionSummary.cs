using System.Diagnostics;
using System.Text.Json.Serialization;

namespace GP.Utilix;

public class DatabaseActionSummary
{
    public string ActionName { get; set; } = "";
    
    public long Milliseconds { get; set; }
}

public static class StopWatchExtensions
{
    public static DatabaseActionSummary ToDatabaseActionSummary(this Stopwatch stopwatch, string name)
    {
        return new DatabaseActionSummary
        {
            ActionName = name,
            Milliseconds = stopwatch.ElapsedMilliseconds
        };
    }
}