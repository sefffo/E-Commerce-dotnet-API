using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule
{
    public enum OrderStatus : byte
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5,
        PaymentPending = 6,   // order created, waiting for payment
        Paid = 7,             // Fawaterak confirmed payment
        PaymentReceived = 8,  // payment captured, awaiting fulfilment
        Preparing = 9         // order is being prepared for shipment
    }
}
