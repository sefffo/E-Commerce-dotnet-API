using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.SharedLibirary.DTO_s.OrderDTOs
{
    public record OrderDTO(string BasketId , int DeliveryMethoId , AddressDTO Address);


}
