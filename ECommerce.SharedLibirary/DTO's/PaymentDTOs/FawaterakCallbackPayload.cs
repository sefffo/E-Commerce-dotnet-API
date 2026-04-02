using System.Text.Json.Serialization;

namespace ECommerce.SharedLibirary.DTO_s.PaymentDTOs
{
    public class FawaterakCallbackPayload
    {
        // Fawaterak sends "invoice_id" in snake_case
        [JsonPropertyName("invoice_id")]
        public string? InvoiceId { get; set; }

        // Fawaterak sends "payment_status"
        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        // Extra fields Fawaterak may send
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }
}
