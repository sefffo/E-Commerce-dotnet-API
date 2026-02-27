using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.MappingProfiles
{
    public class OrderProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }

            if (source.Product.PictureUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return source.Product.PictureUrl;
            }

            var baseurl = configuration.GetSection("URLs")["BaseUrl"];

            if (baseurl == null)
                return string.Empty;

            var picUrl = $"{baseurl}{source.Product.PictureUrl}";


            return picUrl;
        }
    }
}
