namespace Newme.Catalog.Domain.Entities
{
    public record Discount {
        private Discount(){}
        public Discount(
            decimal percentage, 
            Money price)
        {
            Percentage = percentage;
            Price = price;
        }

        public decimal Percentage { get; init; }
        public Money Price { get; init; }
    }}