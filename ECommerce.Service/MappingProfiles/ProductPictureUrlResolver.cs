using AutoMapper;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.SharedLibirary.DTO_s.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver (IConfiguration configuration)  : IValueResolver<Product, ProductDto, string>
    {


        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            var picturePath = source.PictureUrl;
            if (picturePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                var uri = new Uri(picturePath);
                picturePath = uri.PathAndQuery;
            }
            var baseurl = configuration.GetSection("URLs")["BaseUrl"];

            if(baseurl == null)
                return string.Empty;

            return $"{baseurl.TrimEnd('/')}{picturePath}";

        }
    }
}
