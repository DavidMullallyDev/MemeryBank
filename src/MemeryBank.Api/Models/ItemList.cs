namespace MemeryBank.Api.Models
{
    public class ItemList
    {
        public string? ListTitle { get; set; }
        public List<string>? ListItems { get; set; }
        public bool? IsOrderedList { get; set; } = false;
    }
}
