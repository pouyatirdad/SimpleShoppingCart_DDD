using ShoppingCart_Domain.Common;
using ShoppingCart_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
