namespace TMDb.Common
{
    public interface ISorting
    {
        string Column { get; set; }
        bool Order { get; set; }

        string OrderBy();
    }
}