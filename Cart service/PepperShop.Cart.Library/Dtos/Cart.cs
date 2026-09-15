namespace PepperShop.Cart.Library.Dtos
{
    public class Cart
    {
        /// <summary>
        /// The unique identifier for the cart that is inherited from <see cref="Customer.Id"/>.
        /// </summary>
        public string Id { get; set; }

        public List<CartItem>? Items { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Currency { get; set; }

        public Cart(string id)
        {
            Id = id;
        }
    }

    public class CartItem()
    {
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
