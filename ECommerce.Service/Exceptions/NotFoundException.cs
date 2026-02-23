using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Exceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }

    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product with {id} Not Found");

    public sealed class BasketNotFoundException(string id) : NotFoundException($"Basket with {id} Not Found");

    public sealed class ProductsNotFoundException() : NotFoundException("Products Not Found in the Database");

    public sealed class BrandsNotFoundException() : NotFoundException("No Brands in the Database");

    public sealed class BrandNotFoundException(int id) : NotFoundException($"Brand with {id} Not Found");

    public sealed class TypesNotFoundException() : NotFoundException("No Types in the Database");

    public sealed class typeNotFoundException(int id) : NotFoundException($"Type with {id} Not Found");

}
