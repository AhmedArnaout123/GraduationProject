﻿namespace GP.ECommerce1.Core.Domain;

public class Review
{
    public Guid Id { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public int Rate { get; set; }
    
    public string Comment { get; set; } = "";
}