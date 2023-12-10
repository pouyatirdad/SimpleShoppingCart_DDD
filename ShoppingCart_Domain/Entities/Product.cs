using ShoppingCart_Domain.ValueObjects;

namespace ShoppingCart_Domain.Entities
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public Price Price { get; }

        public Product(Guid id, string name, Price price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}