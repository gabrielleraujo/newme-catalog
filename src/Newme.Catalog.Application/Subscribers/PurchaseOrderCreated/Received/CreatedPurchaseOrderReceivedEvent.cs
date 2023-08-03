using Newtonsoft.Json;

namespace Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Received
{
    public class CreatedPurchaseOrderReceivedEvent 
    {
        public CreatedPurchaseOrderReceivedEvent(Guid id, Guid purchaseId, IList<CreatedPurchaseItemOrderReceivedEvent> items)
        {
            Id = id;
            PurchaseId = purchaseId;
            Items = items;
        }

        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("purchase_id")]
        public Guid PurchaseId { get; private set; }

        [JsonProperty("terms")]
        public IList<CreatedPurchaseItemOrderReceivedEvent> Items { get; private set; }
    }
}