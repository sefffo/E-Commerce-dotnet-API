using ECommerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications
{
    public class OrderSpecifications : BaseSpecification<Order, Guid>
    {
        public OrderSpecifications(string Email) : base(o => o.UserEmail == Email)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);

            OrderByDescindingExpression(o => o.OrderDate);
        }

        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);

        }




    }
}
