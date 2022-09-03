using AutoMapper;
using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.ECommerce1.Core.Domain;

namespace GP.ECommerce1.Infrastructure.MongoDb;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCategoryCommand, MongoEntities.Category>();

        CreateMap<MongoEntities.Category, Category>()
            .ReverseMap();


        CreateMap<Address, MongoEntities.Address>()
            .ReverseMap();

        CreateMap<CreateCustomerCommand, MongoEntities.Customer>();

        CreateMap<MongoEntities.Customer, Customer>()
            .ReverseMap();

        CreateMap<AddAddressCommand, MongoEntities.Address>();

        
        CreateMap<CreateDiscountCommand, MongoEntities.Discount>();

        CreateMap<MongoEntities.Discount, Discount>()
            .ReverseMap();

        CreateMap<CreateProductCommand, MongoEntities.Product>();

        CreateMap<MongoEntities.Product, Product>()
            .ReverseMap();

        CreateMap<CreateReviewCommand, MongoEntities.Review>();

        CreateMap<MongoEntities.Review, Review>()
            .ReverseMap();

        CreateMap<MongoEntities.CartItem, CartItem>()
            .ReverseMap();
        CreateMap<MongoEntities.ShoppingCart, ShoppingCart>()
            .ReverseMap();

        CreateMap<OrderItem, MongoEntities.OrderItem>()
            .ReverseMap();

        CreateMap<CreateOrderCommand, MongoEntities.Order>();
        CreateMap<Order, MongoEntities.Order>()
            .ReverseMap();
    }
}