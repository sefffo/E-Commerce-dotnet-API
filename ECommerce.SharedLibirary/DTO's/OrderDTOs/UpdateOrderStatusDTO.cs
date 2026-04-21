using System.ComponentModel.DataAnnotations;

namespace ECommerce.SharedLibirary.DTO_s.OrderDTOs
{
    /// <summary>
    /// Payload for PATCH /api/Order/{id}/status.
    /// Status must be one of the <see cref="Domain.Entities.OrderModule.OrderStatus"/> names
    /// (e.g. "Paid", "Preparing", "Shipped", "Delivered", "Cancelled").
    /// </summary>
    public class UpdateOrderStatusDTO
    {
        [Required]
        public string Status { get; set; } = default!;
    }
}
