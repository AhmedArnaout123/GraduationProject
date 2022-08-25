using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Orders.Queries.GetOrdersStatuses;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Orders.QueriesHandler.GetOrdersStatuses;

public class GetOrderStatusesQueryHandler : IRequestHandler<GetOrderStatusesQuery, Result<List<OrderStatus>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetOrderStatusesQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<OrderStatus>>> Handle(GetOrderStatusesQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<OrderStatus>> {IsSuccess = true};
        List<OrderStatus> orderStatuses = new();
        string stmt = "SELECT * FROM OrderStatuses";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                var orderStatus = new OrderStatus
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    Status = Convert.ToString(reader["Status"])!,
                };
                orderStatuses.Add(orderStatus);
            }

            result.Value = orderStatuses;
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