using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Guid CustomerId { get; set; }

    public int Rate { get; set; }

    public string Comment { get; set; } = "";
    
    public DateTime Date { get; set; }

    public string CustomerName { get; set; } = "";
}