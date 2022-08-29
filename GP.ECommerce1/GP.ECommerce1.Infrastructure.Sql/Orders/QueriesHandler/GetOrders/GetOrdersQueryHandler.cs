using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Application.Orders.Queries.GetOrders;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Orders.QueriesHandler.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<Order>>>
{
    private readonly SqlConnection _sqlConnection;
    private readonly IMediator _mediator;

    public GetOrdersQueryHandler(SqlConnection sqlConnection, IMediator mediator)
    {
        _sqlConnection = sqlConnection;
        _mediator = mediator;
    }

    public async Task<Result<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Order>> {IsSuccess = true};
        List<Order> orders = new();
        string stmt = @"SELECT Orders.Id, Date, Orders.CustomerId, Orders.AddressId, Status, CONCAT(Customers.FirstName, ' ', Customers.LastName) as 'CustomerName',Country, State, City, Street1, Street2
                         FROM Orders
                         INNER JOIN Customers ON Customers.Id = Orders.CustomerId
                         INNER JOIN Addresses ON Addresses.Id = Orders.AddressId";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                var order = new Order
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    Date = Convert.ToDateTime(reader["Date"]),
                    CustomerId = Guid.Parse(Convert.ToString(reader["CustomerId"])!),
                    Status = (Convert.ToString(reader["Status"])!).ToOrderStatus(),
                    CustomerName = Convert.ToString(reader["CustomerName"])!,
                    Address = new Address()
                    {
                        City = Convert.ToString(reader["City"])!,
                        Country = Convert.ToString(reader["Country"])!,
                        State = Convert.ToString(reader["State"])!,
                        Street1 = Convert.ToString(reader["Street1"])!,
                        Street2 = Convert.ToString(reader["Street2"])!,
                        Id = Guid.Parse(Convert.ToString(reader["AddressId"])!),
                        CustomerId = Guid.Parse(Convert.ToString(reader["CustomerId"])!),
                    }
                };
                string stmt2 = @"SELECT ProductName, ProductPrice, Quantity, ProductId, DiscountId,
                                    Discounts.Description,
                                    Discounts.Percentage
                                 FROM Orders_Products 
                                 INNER JOIN Discounts ON Discounts.Id=DiscountId
                                 WHERE OrderId=@OrderId";
                var command2 = new SqlCommand(stmt2, _sqlConnection);
                command2.Parameters.AddWithValue("@OrderId", order.Id);
                var reader2 = await command2.ExecuteReaderAsync(cancellationToken);
                while (reader2.Read())
                {
                    Guid? discountId = Guid.TryParse(Convert.ToString(reader2["DiscountId"]), out var r)
                        ? r
                        : null;
                    Discount? discount = null;
                    if (discountId != null)
                    {
                        discount = new Discount
                        {
                            Description = Convert.ToString(reader2["Description"])!,
                            Id = discountId.Value,
                            Percentage = Convert.ToInt32(reader2["Percentage"])
                        };
                    }

                    var orderItem = new OrderItem
                    {
                        Quantity = Convert.ToInt32(reader2["Quantity"]),
                        ProductName = Convert.ToString(reader2["ProductName"])!,
                        ProductPrice = Convert.ToDouble(reader2["ProductPrice"]),
                        OrderId = order.Id,
                        ProductId = Guid.Parse(Convert.ToString(reader2["ProductId"])!),
                        Discount = discount
                    };
                    order.Items.Add(orderItem);
                }

                orders.Add(order);
                command2.Dispose();
            }

            result.Value = orders;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result.IsSuccess = false;
            result.Error = $"Exception: {ex.Message}";
        }
        finally
        {
            _sqlConnection.Close();
        }

        return result;
    }
}