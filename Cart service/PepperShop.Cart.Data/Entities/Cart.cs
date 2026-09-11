namespace PepperShop.Cart.Data.Entities
{
    public class Cart
    {
        public string id { get; set; }

        public List<CartItem>? Items { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public Cart(string cartId,
            List<CartItem>? items = null,
            decimal discount = 0,
            decimal totalPrice = 0)
        {
            id = cartId;
            Items = items;
            Discount = discount;
            TotalPrice = totalPrice;
        }
    }

    public class CartItem()
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
