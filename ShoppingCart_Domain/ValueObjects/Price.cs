namespace ShoppingCart_Domain.ValueObjects
{
    //ValueObject of ShoppingCart
    public class Price
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
    }
}