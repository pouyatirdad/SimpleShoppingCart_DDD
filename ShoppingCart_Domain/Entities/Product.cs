using ShoppingCart_Domain.Common;
using ShoppingCart_Domain.ValueObjects;

namespace ShoppingCart_Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }

        public Product()
        {
        }

        public Product(Guid id, string name, Price price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}