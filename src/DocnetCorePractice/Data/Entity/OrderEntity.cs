using DocnetCorePractice.Enum;
using DocnetCorePractice.Model;

namespace DocnetCorePractice.Data.Entity
{
    public class OrderEntity : Entity
    {
        public string? userId { get; set; }
        public string? orderId { get; set; }
        public decimal total 
        {
            get
            {
                return itemss.Sum(s => s.Price) * itemss.Sum(s => s.volumn);
            }
        }
        public List<OrderItemEntity> itemss { get; set; }

    }
}
