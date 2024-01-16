namespace Common.Models.Response
{
    public class MainResponse<T>
    {
        public T? Response { get; set; }
        public string? Message { get; set; } = string.Empty;
        public bool Error { get; set; } = false;
    }
}
