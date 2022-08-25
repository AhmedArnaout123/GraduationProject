﻿using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double Subtotal { get; set; }
    public Guid CustomerId { get; set; }
    
    public Guid StatusId { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}