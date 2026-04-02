using System.Text.Json.Serialization;

namespace ECommerce.SharedLibirary.DTO_s.PaymentDTOs
{
    public class FawaterakCallbackPayload
    {
        [JsonPropertyName("invoice_id")]
        public string? InvoiceId { get; set; }

        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; set; } // "paid", "failed", "pending"

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }
    }
}
