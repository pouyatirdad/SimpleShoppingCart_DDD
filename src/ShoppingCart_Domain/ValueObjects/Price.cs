
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;

namespace ShoppingCart_Domain.ValueObjects
{
    //ValueObject of ShoppingCart
    public class Price:ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal Amount, string Currency)
        {
            SetAmount(Amount);
            SetCurrency(Currency);
        }

        private void SetAmount(decimal amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            this.Amount = amount;
        }

        private void SetCurrency(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                currency = "Rial";
            }
            this.Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}