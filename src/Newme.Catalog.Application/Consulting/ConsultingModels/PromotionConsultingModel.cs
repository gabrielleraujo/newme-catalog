using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class PromotionConsultingModel : ConsultingModel
    {
        public PromotionConsultingModel(
            Guid id,
            Guid productId,
            string name,
            DateTime start,
            DateTime end,
            decimal percentage,
            string currency,
            decimal amount,
            string description
        ) : base(id)
        {
            ProductId = productId;
            Name = name;
            Start = start;
            End = end;
            Percentage = percentage;
            Currency = currency;
            Amount = amount;
            Description = description;
        }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public decimal Percentage { get; private set; }
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }

    
        public static PromotionConsultingModel MapFromDomain(Promotion entity) 
        {
            var consultingModel = new PromotionConsultingModel(
                entity.Id.value,
                entity.ProductId.value,
                entity.Name,
                entity.Period.start,
                entity.Period.end,
                entity.Discount.Percentage,
                entity.Discount.Price.Currency,
                entity.Discount.Price.Amount,
                entity.Description);

            MapFromDomain(consultingModel, entity);
            return consultingModel;
        }

        public static Promotion MapToDomain(PromotionConsultingModel consultionModel) 
        {
            var entity = new Promotion(
                new PromotionId(consultionModel.Id),
                new ProductId(consultionModel.ProductId),
                consultionModel.Name,
                new Period(
                    consultionModel.Start,
                    consultionModel.End),
                new Discount(consultionModel.Percentage, 
                    new Money(
                        consultionModel.Currency, 
                        consultionModel.Amount)),
                consultionModel.Description);

            Entity.SetOriginalInformationWhenMapToDomain(
                entity,
                consultionModel.CreateDate,
                consultionModel.LastUpdateDate,
                consultionModel.Active
            );

            return entity;
        }
    }
}