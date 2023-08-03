using Newtonsoft.Json;

namespace Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Sent
{
    public class ReducedProductsStockSentEvent
    {
        public ReducedProductsStockSentEvent()
        {
            Id = Guid.NewGuid();
            Success = false;
            _items = new List<ReducedProductItemStockSentEvent>();
        }

        public ReducedProductsStockSentEvent(
            Guid purchaseId)
        {
            Id = Guid.NewGuid();
            PurchaseId = purchaseId;
            Success = true;
            _items = new List<ReducedProductItemStockSentEvent>();
        }

        [JsonIgnore]
        public static string EventName = "catalog-reduced-product-stock";

        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("purchase_id")]
        public Guid PurchaseId { get; private set; }

        [JsonProperty("success")]
        public bool Success { get; private set; }

        [JsonIgnore]
        private IList<ReducedProductItemStockSentEvent> _items;

        [JsonProperty("items")]
        public IReadOnlyCollection<ReducedProductItemStockSentEvent> Items => _items.ToList();

        public void AddItem(ReducedProductItemStockSentEvent item)
        {
            _items.Add(item);
        }
    }
}