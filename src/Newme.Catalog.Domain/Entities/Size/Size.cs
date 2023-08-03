namespace Newme.Catalog.Domain.Entities
{
    public class Size : Differential
    {
        private Size() {}
        public Size(SizeId id, string name) : base(name)
        {
            Id = id;
        }

        public SizeId Id { get; private set; }
        public override void FixName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"Invalid size name {name}.");
            Name = name;
        }
    }
}