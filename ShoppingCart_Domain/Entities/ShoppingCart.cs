using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart_Domain.Common;
using ShoppingCart_Domain.ValueObjects;

namespace ShoppingCart_Domain.Entities
{
    //AggregateRoot
    public class ShoppingCart : BaseEntity
    {
        public Guid Id { get; private set; }
        public List<Product> Items { get; private set; }
        public Price Total { get; private set; }

        public ShoppingCart()
        {
                
        }
        public ShoppingCart(Guid id)
        {
            Id = id;
            Items = new List<Product>();
            Total = new Price(0, "Rial");
        }

        public void AddItem(Product item)
        {
            Items.Add(item);
            Total = new Price(Total.Amount + item.Price.Amount, Total.Currency);
        }

        public void RemoveItem(Product item)
        {
            Items.Remove(item);
            Total = new Price(Total.Amount - item.Price.Amount, Total.Currency);
        }

        public void Clear()
        {
            Items.Clear();
            Total = new Price(0, "Rial");
        }
    }
}
