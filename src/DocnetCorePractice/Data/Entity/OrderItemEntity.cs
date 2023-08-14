using DocnetCorePractice.Model;

namespace DocnetCorePractice.Data.Entity
{
    public class OrderItemEntity : Entity
    {
        public string? name { get; set; }
        public decimal unitPrice { get; set; }
        public decimal volumn { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
