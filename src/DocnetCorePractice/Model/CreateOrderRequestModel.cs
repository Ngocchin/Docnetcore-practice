namespace DocnetCorePractice.Model
{
    public class Item 
    {
        public string? caffeeId { get; set; }
        public decimal volumn { get; set; }
    }
    public class CreateOrderRequestModel
    {
        public string? userId { get; set; }
        public List<Item>? items { get; set; } 
    }
}
