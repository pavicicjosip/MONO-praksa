namespace TMDb.Common
{
    public interface ISort
    {
        string Column { get; set; }
        bool Order { get; set; }
        string OrderBy();
    }
}