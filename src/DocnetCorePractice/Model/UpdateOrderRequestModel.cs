namespace DocnetCorePractice.Model
{
    public class UpdateOrderRequestModel
    {
        //{
        //  "orderId": "string",
        //  "addItems": [
        //    {
        //      "caffeeId": "string",
        //      "volumn": 0,
        //    }
        //  ],
        //  "updateItems": [
        //    {
        //      "orderItemId": "string",
        //      "volumn": 0
        //    }
        //  ],
        //  "removeItems": [
        //    {
        //      "orderItemId": "string"
        //    }
        //  ]
        //}
        public string OrderId { get; set; }
        public List<addItems> addItems { get; set; } = null!;
        public List<updateItems> updateItems { get; set; } = null!;
        public List<removeItems> removeItems { get; set; } = null!;
    }
    public class addItems
    {
        public string caffeeId { get; set; } = null!;
        public int volumn { get; set; }
    }
    public class updateItems
    {
        public string orderItemId { get; set; } = null!;
        public int volumn { get; set; }
    }
    public class removeItems
    {
        public string orderItemId { get; set; } = null!;
    }
}
