namespace Newme.Catalog.Domain.Entities
{
    public class Product : Entity
    {
        private Product() {}
        public Product(
            ProductId id,
            string name, 
            Sku sku,
            Money price, 
            string description,
            DifferentialGroup differentialGroup,
            int stock)
        {
            Id = id;
            Name = name;
            Sku = sku;
            Price = price;
            Description = description;
            DifferentialGroup = differentialGroup;
            Stock = stock;
            Promotion = null;
        }

        public ProductId Id { get; private set; }
        public string Name { get; private set; }
        public Sku Sku { get; private set; }
        public Money Price { get; private set; }
        public string Description { get; private set; }
        public DifferentialGroup DifferentialGroup { get; private set; }
        public int Stock { get; private set; }
        public Promotion? Promotion { get; private set; }
        
        public override void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }

        public Product FixName(string name)
        {
            if (name.Length < 4) throw new ArgumentException($"Invalid category name {name}.");
            Name = name;
            return this;
        }

        public Product FixDescription(string description)
        {
            if (description.Length < 10) throw new ArgumentException($"Invalid category name {description}.");
            Description = description;
            return this;
        }
        
        public void ChangePrice(Money price)
        {
            Price = price;
        }

        public void IncreaseStock(int quantity)
        {
            if (Stock > quantity) throw new ArgumentException($"Invalid quantity {quantity}, quantity is less than new stock value.");
            Stock = quantity;
        }
        
        // Return quantity achieved
        public int ReduceStock(int quantity)
        {
            if (quantity < 0) throw new ArgumentException($"Invalid quantity {quantity}, quantity is less than 0.");
            
            var newStock = Stock - quantity;
            
            if (newStock < 0)
            {
                Stock = 0;
                Active = false;
                return quantity + newStock;
            }

            Stock = quantity;
            return quantity;
        }

        public void AddPromotion(Promotion promotion)
        {
            Promotion = promotion;
        }

        public void RemovePromotion(Promotion promotion)
        {
            Promotion = null;
        }
        
        // public (int, bool, bool) CalculateNewStock(
        //     int requiredQuantity)
        // {
        //     var newStock = Stock - requiredQuantity;
        //     bool hasOutOfStok = false;
        //     bool isEmptyStock = false;
            
        //     if (Stock == 0) 
        //     {
        //         isEmptyStock = true;
        //         return new(Stock, isEmptyStock, hasOutOfStok);
        //     }
        //     if (newStock < 0)
        //     {
        //         newStock = 0;
        //         hasOutOfStok = true;
        //         return new(newStock, isEmptyStock, hasOutOfStok);
        //     }

        //     return new(newStock, isEmptyStock, hasOutOfStok);
        // }
    }
}