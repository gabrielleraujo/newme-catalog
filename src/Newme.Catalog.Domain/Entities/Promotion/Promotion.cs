namespace Newme.Catalog.Domain.Entities
{
    public class Promotion : Entity
    {
        private Promotion() {}
        public Promotion(
            PromotionId id,
            ProductId productId,
            string name,
            Period period,
            Discount discount,
            string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Period = period;
            Discount = discount;
            Active = false;
            Description = description;
        }

        public PromotionId Id { get; private set; }
        public ProductId ProductId { get; private set; }
        public string Name { get; private set; }
        public Period Period { get; private set; }
        public Discount Discount { get; private set; }
        public string Description { get; private set; }

        public Promotion FixName(string name)
        {
            Name = name;
            return this;
        }

        public Promotion FixDescription(string name)
        {
            Name = name;
            return this;
        }

        public override void Activate()
        {
            Active = true;
        }

        public override void Deactivate()
        {
            Active = false;
        }
    }
}