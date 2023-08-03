using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class ProductConsultingModel : ConsultingModel
    {
        public ProductConsultingModel(
            Guid id,
            string name,
            Guid sku,
            string currency,
            decimal amount,
            string description,
            GenderConsultingModel gender,
            CategoryConsultingModel category,
            ColorConsultingModel color,
            SizeConsultingModel size,
            int stock,
            PromotionConsultingModel? promotion
        ) : base(id)
        {
            Name = name;
            Sku = sku;
            Currency = currency;
            Amount = amount;
            Description = description;
            Gender = gender;
            Category = category;
            Color = color;
            Size = size;
            Stock = stock;
            Promotion = promotion;
            Images = new List<string>();
        }

        public Guid Sku { get; private set; }
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public GenderConsultingModel Gender { get; private set; }
        public CategoryConsultingModel Category { get; private set; }
        public ColorConsultingModel Color { get; private set; }
        public SizeConsultingModel Size { get; private set; }
        public int Stock { get; private set; }
        public PromotionConsultingModel? Promotion { get; private set; }
        public IList<string> Images { get; private set; }

        public string CreateImagePath() => $"products/{Name
            .ToLower()
            .Replace(" ", "-")}/{Guid.NewGuid()}";
            
        public void AddImage(string image)
        {
            Images.Add(image);
        }

        public static ProductConsultingModel MapFromDomain(
            Product entity, 
            GenderConsultingModel gender,
            CategoryConsultingModel category, 
            ColorConsultingModel color, 
            SizeConsultingModel size) 
        {
            var consultingModel = new ProductConsultingModel(
                entity.Id.value,
                entity.Name,
                entity.Sku.value,
                entity.Price.Currency,
                entity.Price.Amount,
                entity.Description,
                gender,
                category,
                color,
                size,
                entity.Stock,
                entity.Promotion == null ? null : PromotionConsultingModel.MapFromDomain(entity.Promotion)
                );

            MapFromDomain(consultingModel, entity);
            return consultingModel;
        }

        public Product MapToDomain() 
        {
            var entity = new Product(
                new ProductId(Id),
                Name,
                new Sku(Sku),
                new Money(Currency, Amount),
                Description,
                new DifferentialGroup(
                    new CategoryId(Category.Id), 
                    new ColorId(Color.Id), 
                    new SizeId(Size.Id),
                    new GenderId(Gender.Id)),
                Stock);

            if (Promotion != null) 
            {
                entity.AddPromotion(promotion: PromotionConsultingModel.MapToDomain(Promotion));
            }
            
            Entity.SetOriginalInformationWhenMapToDomain(
                entity,
                CreateDate,
                LastUpdateDate,
                Active
            );

            return entity;
        }
    }
}
