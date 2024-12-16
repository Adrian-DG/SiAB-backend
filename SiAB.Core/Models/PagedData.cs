namespace SiAB.Core.Models;

public class PagedData<T> where T : class
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T>? Rows { get; set; }
}