using AutoMapper;
using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.ECommerce1.Core.Domain;

namespace GP.ECommerce1.Infrastructure.MongoDb;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCategoryCommand, MongoEntities.Category>()
            .ForMember(c => c.Id, option => option.MapFrom(c => c.Id.ToString()))
            .ForMember(c => c.ParentId, option => option.MapFrom(c => c.ParentId.ToString()));

        CreateMap<MongoEntities.Category, Category>()
            .ForMember(c => c.Id, option => option.MapFrom(c => Guid.Parse(c.Id)))
            .ForMember(c => c.ParentId,
                option => option.MapFrom<Guid?>(c => c.ParentId != null ? Guid.Parse(c.ParentId) : null));

        CreateMap<CreateCustomerCommand, MongoEntities.Customer>()
            .ForMember(c => c.Id, option => option.MapFrom(c => c.Id.ToString()));
        CreateMap<MongoEntities.Customer, Customer>()
            .ForMember(c => c.Id, option => option.MapFrom(c => Guid.Parse(c.Id)));

        CreateMap<AddAddressCommand, MongoEntities.Address>();

        CreateMap<CreateDiscountCommand, MongoEntities.Discount>()
            .ForMember(c => c.Id, option => option.MapFrom(c => c.Id.ToString()));
        CreateMap<MongoEntities.Discount, Discount>()
            .ForMember(c => c.Id, option => option.MapFrom(c => Guid.Parse(c.Id)));

        CreateMap<CreateReviewCommand, MongoEntities.Review>()
            .ForMember(c => c.Id, option => option.MapFrom(c => c.Id.ToString()))
            .ForMember(c => c.CustomerId, option => option.MapFrom(c => c.CustomerId.ToString()))
            .ForMember(c => c.ProductId, option => option.MapFrom(c => c.ProductId.ToString()));

        CreateMap<MongoEntities.Review, Review>()
            .ForMember(c => c.Id, option => option.MapFrom(c => Guid.Parse(c.Id)))
            .ForMember(c => c.CustomerId, option => option.MapFrom(c => Guid.Parse(c.CustomerId)))
            .ForMember(c => c.ProductId, option => option.MapFrom(c => Guid.Parse(c.ProductId)));

        CreateMap<CreateProductCommand, MongoEntities.Product>()
            .ForMember(c => c.Id, option => option.MapFrom(c => c.Id.ToString()))
            .ForMember(c => c.Category, option => option.MapFrom((c) =>
                new MongoEntities.Category
                {
                    ParentId = c.CategoryParentId.ToString(),
                    Id = c.CategoryId.ToString(),
                    Name = c.CategoryName
                }
            ))
            .ForMember(c => c.Discount, option => option.MapFrom(c => new MongoEntities.Discount
            {
                Description = c.DiscountDescription,
                Id = c.DiscountId.ToString() ?? "",
                Percentage = c.DiscountPercentage
            }));

        // CreateMap<MongoEntities.Product, Product>()
        //     .ForMember(c => c.Id, option => option.MapFrom(c => Guid.Parse(c.Id)))
        //     .ForMember(c => c.CategoryId, option => option.MapFrom(c => c.Category.Id))
        //     .ForMember(c => c.CategoryName, option => option.MapFrom(c => c.Category.Name))
        //     .ForMember(c => c., option => option.MapFrom(c => Guid.Parse(c.ProductId)));
    }
}