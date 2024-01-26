namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {

        public int Quantity { get; set; }
        public string Color { get; set; }

        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }


    
        public ShoppingCartItem(int quantity, string color, decimal price, string productId, string productName)
        {
            Quantity = quantity;
            Color = color;
            Price = price;
            ProductId = productId;
            ProductName = productName;
        }

        public decimal TotalPrice => Price * Quantity;

    }
    
}