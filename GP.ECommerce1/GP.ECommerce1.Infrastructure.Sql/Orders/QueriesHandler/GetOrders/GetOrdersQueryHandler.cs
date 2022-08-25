using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Orders.Queries.GetOrders;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Orders.QueriesHandler.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<Order>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetOrdersQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Order>> {IsSuccess = true};
        List<Order> orders = new();
        string stmt = @"SELECT Orders.Id, Date, Subtotal, CustomerId, OrderStatusId, OrderStatuses.Status 
                         FROM Orders
                         INNER JOIN OrderStatuses ON OrderStatuses.Id = Orders.OrderStatusId";
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
                    Subtotal = Convert.ToDouble(reader["Subtotal"]),
                    CustomerId = Guid.Parse(Convert.ToString(reader["CustomerId"])!),
                    StatusId = Guid.Parse(Convert.ToString(reader["OrderStatusId"])!),
                    Status = Convert.ToString(reader["Status"])!
                };
                string stmt2 = "SELECT * FROM Orders_Products WHERE OrderId=@OrderId";
                var command2 = new SqlCommand(stmt2, _sqlConnection);
                command2.Parameters.AddWithValue("@OrderId", order.Id);
                var reader2 = await command2.ExecuteReaderAsync(cancellationToken);
                while (reader2.Read())
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = Convert.ToInt32(reader2["Quantity"]),
                        ProductName = Convert.ToString(reader2["ProductName"])!,
                        ProductPrice = Convert.ToDouble(reader2["ProductPrice"]),
                        ProductSubtotal = Convert.ToDouble(reader2["ProductSubtotal"]),
                        OrderId = order.Id,
                        ProductId = Guid.Parse(Convert.ToString(reader2["ProductId"])!),
                        DiscountId = Guid.TryParse(Convert.ToString(reader2["DiscountId"]), out var r)
                            ? r
                            : null,
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