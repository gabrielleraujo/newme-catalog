namespace Newme.Catalog.Application.Commands
{
    public abstract class ProductCommand : Command
    {
        public string Name { get; protected set; }
        public string Currency { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; }
        public Guid GenderId { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public Guid ColorId { get; protected set; }
        public Guid SizeId { get; protected set; }
        public int Stock { get; protected set; }
    }
}