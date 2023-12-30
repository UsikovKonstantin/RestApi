﻿namespace Domain;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Weight { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
