using Newtonsoft.Json;

namespace Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Sent
{
    public class ReducedProductItemStockSentEvent 
    {
        public ReducedProductItemStockSentEvent(
            Guid productId, 
            int quantityAchieved)
        {
            ProductId = productId;
            QuantityAchieved = quantityAchieved;
        }

        [JsonProperty("product_id")]
        public Guid ProductId { get; private set; }

        [JsonProperty("quantity_achieved")]
        public int QuantityAchieved { get; private set; }
    }
}
