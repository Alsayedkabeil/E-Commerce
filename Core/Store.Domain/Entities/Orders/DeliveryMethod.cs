namespace Store.Domain.Entities.Orders
{
    // Mapping To Table
    public class DeliveryMethod : BaseEntity<int> // PK
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}