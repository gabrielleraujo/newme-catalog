namespace Newme.Catalog.Domain.Entities
{
    public class Category : Differential
    {
        private Category() {}
        public Category(CategoryId id, string name) : base(name)
        {
            Id = id;
        }

        public CategoryId Id { get; private set; }

        public override void FixName(string name)
        {
            if (name.Length < 3) throw new ArgumentException($"Invalid category name {name}.");
            Name = name;
        }
    }
}