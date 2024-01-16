namespace Common.Models.Response
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Rows { get; set; }
        public int Count { get; set; }
    }
}
