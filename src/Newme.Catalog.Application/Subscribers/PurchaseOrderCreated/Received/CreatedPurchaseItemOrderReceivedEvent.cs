using Newtonsoft.Json;

namespace Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Received
{
    public class CreatedPurchaseItemOrderReceivedEvent 
    {
        public CreatedPurchaseItemOrderReceivedEvent(Guid id, int quantity, Guid productId)
        {
            Id = id;
            Quantity = quantity;
            ProductId = productId;
        }

        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("quantity")]
        public int Quantity { get; private set; }

        [JsonProperty("product_id")]
        public Guid ProductId { get; private set; }
    }
}