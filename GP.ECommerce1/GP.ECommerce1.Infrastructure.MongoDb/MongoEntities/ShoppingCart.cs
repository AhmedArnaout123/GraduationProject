﻿using MongoDB.Bson.Serialization.Attributes;

namespace GP.ECommerce1.Infrastructure.MongoDb.MongoEntities;

public class ShoppingCart
{
    [BsonId]
    public Guid CustomerId { get; set; }
    
    public List<CartItem> Items { get; set; } = new();
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = "";

    public string ProductMainImageUri { get; set; } = "";
    
    public double ProductPrice { get; set; }
    public int Quantity { get; set; }
} 