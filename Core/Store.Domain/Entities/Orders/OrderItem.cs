namespace Store.Domain.Entities.Orders
{
    // Mapping To Table
    public class OrderItem : BaseEntity<int> // PK
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrderItem Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}