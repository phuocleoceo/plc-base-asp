namespace PlcBase.Base.DTO;

public class PagedList<T>
{
    public List<T> Records { get; set; }

    public int TotalRecords { get; set; }

    public PagedList(List<T> items, int count)
    {
        Records = items;
        TotalRecords = count;
    }
}
