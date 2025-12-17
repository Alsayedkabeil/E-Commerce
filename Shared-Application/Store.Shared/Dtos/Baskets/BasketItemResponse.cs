using System.ComponentModel.DataAnnotations;

namespace Store.Shared.Dtos.Baskets
{
    public class BasketItemResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0,99)]
        public int Quantity { get; set; }
    }
}