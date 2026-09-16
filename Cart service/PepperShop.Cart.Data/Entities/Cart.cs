namespace PepperShop.Cart.Data.Entities
{
    public class Cart
    {
        public string id { get; set; }

        public List<CartItem>? Items { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Currency { get; set; }
    }

    public class CartItem()
    {
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
