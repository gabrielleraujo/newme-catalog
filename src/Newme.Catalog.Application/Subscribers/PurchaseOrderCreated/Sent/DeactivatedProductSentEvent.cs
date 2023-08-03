using Newtonsoft.Json;

namespace Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Sent
{
    public class DeactivatedProductSentEvent
    {
        public DeactivatedProductSentEvent()
        {
            Id = Guid.NewGuid();
            Success = false;
        }

        public DeactivatedProductSentEvent(
            Guid productId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Success = true;
        }

        [JsonIgnore]
        public static string EventName = "catalog-deactivated-product";

        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("product_id")]
        public Guid ProductId { get; private set; }

        [JsonProperty("success")]
        public bool Success { get; private set; }
    }
}