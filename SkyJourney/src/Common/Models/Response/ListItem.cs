namespace Common.Models.Response
{
    public class ListItem<T>
    {
        public string Label { get; set; }
        public T Value { get; set; }
    }
}
