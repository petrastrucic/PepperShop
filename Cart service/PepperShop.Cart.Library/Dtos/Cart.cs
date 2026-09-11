namespace PepperShop.Cart.Library.Dtos
{
    internal class Cart
    {
        /// <summary>
        /// The unique identifier for the cart that is inherited from <see cref="Customer.Id"/>.
        /// </summary>
        public string Id { get; set; }

        public List<CartItem>? Items { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        internal Cart(string id)
        {
            Id = id;
        }
    }

    internal class CartItem()
    {
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
