using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.SharedLibirary.DTO_s.OrderDTOs
{
    public record OrderToReturnDTO(
        Guid Id,
        string UserEmail,
        ICollection<OrderItemDTO> OrderItems,
        AddressDTO Address,
        string DeliveryMethod,
        string OrderStatus,
        DateTimeOffset OrderDate,
        decimal SubTotal,
        decimal Total
    );
}
