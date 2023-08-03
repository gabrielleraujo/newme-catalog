namespace Newme.Catalog.Application.Commands
{
    public abstract class PromotionCommand : Command
    {
        public string Name { get; protected set; }
        public decimal Percentage { get; protected set; }
        public string Currency { get; protected set; }
        public decimal Amount { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string Description { get; protected set; }
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
    }
}