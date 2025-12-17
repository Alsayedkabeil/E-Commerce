namespace Store.Domain.Entities.Orders
{
    public enum OrderStatus // Enum for Order Status
    {
        Pending= 0,
        PaymentSuccess= 1,
        PaymentFailure= 2,
    }
}