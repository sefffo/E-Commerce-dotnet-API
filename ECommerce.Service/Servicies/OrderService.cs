using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Domain.Interfaces;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Specifications;
using ECommerce.SharedLibirary.CommonResult;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;

namespace ECommerce.Services.Servicies
{
    public class OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email)
        {
            //the steps for creating an order are:

            //1-mapping the shipping address from the orderDTO to the address entity

            var orderAddress = mapper.Map<OrderShippingAddress>(orderDTO.Address);

            // get the basket throw the redis to get the basket

            var Basket = await basketRepository.GetBasketAsync(orderDTO.BasketId);

            if (Basket == null)
            {
                return Error.NotFound("Basket not found", $"Basket With Id : {orderDTO.BasketId} is Not Found");
            }

            //create a list of order items to be added to the order

            List<OrderItem> OrderItems = new List<OrderItem>();

            foreach (var item in Basket.Items)
            {

                var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);

                if (Product is null)
                {
                    return Error.NotFound("Product not found", $"Product With Id : {item.Id} is Not Found");
                }
                OrderItems.Add(CreateOrderItem(item, Product));
            }

            //check the delivery method id and get the delivery method from the database

            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethoId);

            if (DeliveryMethod is null)
            {
                return Error.NotFound("Delivery method not found", $"Delivery method With Id : {orderDTO.DeliveryMethoId} is Not Found");
            }
            // get the subtotal by multiplying the price of each item by its quantity and then summing them up
            //calc the subtotal on the order items cuz its from the database and we need to make sure that the price is correct and not manipulated from the client side

            var Subtotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //create the order entity and add it to the database

            var Order = new Order
            {
                UserEmail = Email, //htigy men el Token lma ngeb el email
                Items = OrderItems,
                ShippingAddress = orderAddress,
                DeliveryMethod = DeliveryMethod,
                Subtotal = Subtotal


            };

            if (Order is null)
            {
                return Error.InternalServerError("Order creation failed", "An error occurred while creating the order");

            }

            await unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);

            var res = await unitOfWork.SaveChangesAsync();

            if(res == 0)
            {
                return Error.InternalServerError("Order creation failed", "An error occurred while creating the order");
            }

            return mapper.Map<OrderToReturnDTO>(Order);

        }

        private static OrderItem CreateOrderItem(Domain.Entities.BasketModule.BasketItem item, Product Product)
        {
            return new OrderItem
            {
                Product = new ProductItemOrdered
                {
                    ProductId = Product.Id,
                    ProductName = Product.Name,
                    PictureUrl = Product.PictureUrl
                },
                Price = item.Price,
                Quantity = item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTO>>(DeliveryMethods);
        }


        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(orders);
        }

        public async Task<OrderToReturnDTO> GetOrderById(Guid OrderId)
        {
            var Spec = new OrderSpecifications(OrderId);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return mapper.Map<Order, OrderToReturnDTO>(order);

        }
    }
}
