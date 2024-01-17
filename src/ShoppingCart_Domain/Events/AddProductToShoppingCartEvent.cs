using ShoppingCart_Domain.Common;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_Domain.Events
{
    public class AddProductToShoppingCartEvent : BaseEvent
    {
        public AddProductToShoppingCartEvent(ShoppingCart cart)
        {
            Cart = cart;
        }

        public ShoppingCart Cart { get; }
    }

}
