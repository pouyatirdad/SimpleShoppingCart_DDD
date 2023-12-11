
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;

namespace ShoppingCart_Domain.ValueObjects
{
    //ValueObject of ShoppingCart
    public class Price:ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Price(decimal Amount, string Currency)
        {
            if (Amount < 0)
            {
                Amount = 0;
            }
            if (string.IsNullOrWhiteSpace(Currency))
            {
                Currency = "Rial";
            }
            this.Amount = Amount;
            this.Currency = Currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}