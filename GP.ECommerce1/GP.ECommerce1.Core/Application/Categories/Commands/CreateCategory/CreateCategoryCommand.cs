using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    
    public Guid? ParentId { get; set; }
}