namespace GP.ECommerce1.Core.Domain;

public class Discount
{
    public Guid Id { get; set; }

    public int Percentage { get; set; }

    public string Description { get; set; } = "";
}