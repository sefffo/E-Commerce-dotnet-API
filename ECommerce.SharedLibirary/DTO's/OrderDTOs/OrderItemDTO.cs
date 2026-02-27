namespace ECommerce.SharedLibirary.DTO_s.OrderDTOs
{
    public record OrderItemDTO(
        string ProductName,
        string PictureUrl,
        decimal Price,
        int Quantity
    );
}