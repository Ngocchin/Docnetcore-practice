using static DocnetCorePractice.Data.Entity.OrderEntity;

namespace DocnetCorePractice.Model
{
    public class CreateOrderResponseModel
    {
        public string? userId { get; set; }
        public string? orderId { get; set; }
        public decimal total { get; set; }
        public List<Items>? itemss { get; set; }
    }
    public class Items
    {
        public string? name { get; set; }
        public decimal unitPrice { get; set; }
        public decimal volumn { get; set; }
        public double Price { get; set; }
        public decimal Discount { get; set; }
    }
}
