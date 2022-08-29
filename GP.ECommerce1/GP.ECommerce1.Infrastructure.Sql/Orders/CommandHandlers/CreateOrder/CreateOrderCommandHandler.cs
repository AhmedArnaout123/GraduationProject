using System.Data;
using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Orders.Commands.CreateOrder;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Orders.CommandHandlers.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateOrderCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        _sqlConnection.Open();
        var transaction = _sqlConnection.BeginTransaction();
        var command = _sqlConnection.CreateCommand();
        
        try
        {
            command.Transaction = transaction;
            command.CommandText = "INSERT INTO Orders Values(@Id,@Date,@Status,@CustomerId,@AddressId)";
            
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Date", request.Date);
            command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
            command.Parameters.AddWithValue("@Status", request.Status.ToText());
            command.Parameters.AddWithValue("@AddressId", request.Address.Id);
            await command.ExecuteNonQueryAsync();

            command.Parameters.Clear();
            command.CommandText = "INSERT INTO Orders_Products Values(@ProductName, @ProductPrice,@Quantity,@OrderId,@ProductId,@DiscountId)";
            command.Parameters.Add("@ProductName", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductPrice", SqlDbType.Real);
            command.Parameters.Add("@Quantity", SqlDbType.Int);
            command.Parameters.Add("@OrderId", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@DiscountId", SqlDbType.UniqueIdentifier);
            foreach (var item in request.Items)
            {
                command.Parameters["@ProductName"].Value = item.ProductName;
                command.Parameters["@ProductPrice"].Value = item.ProductPrice;
                command.Parameters["@Quantity"].Value = item.Quantity;
                command.Parameters["@ProductId"].Value = item.ProductId;
                command.Parameters["@DiscountId"].Value = (object) item.Discount?.Id ?? DBNull.Value;
                command.Parameters["@OrderId"].Value = request.Id;
                await command.ExecuteNonQueryAsync();
            }
        
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
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