using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;

public class CreateDiscountCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public int Percentage { get; set; }
}