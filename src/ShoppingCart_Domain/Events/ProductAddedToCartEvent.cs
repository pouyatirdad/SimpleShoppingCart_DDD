using ShoppingCart_Domain.Common;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_Domain.Events
{
    public class ProductAddedToCartEvent : BaseEvent
    {
        public ProductAddedToCartEvent(ShoppingCart cart)
        {
            Cart = cart;
        }

        public ShoppingCart Cart { get; }
    }

}
