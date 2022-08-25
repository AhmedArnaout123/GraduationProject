using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.CommandsHandlers.AddProductToShoppingCart;

public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public AddProductToShoppingCartCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Products_ShoppingCarts Values(@CustomerId,@ProductId,@Quantity)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
        command.Parameters.AddWithValue("@ProductId", request.ProductId);
        command.Parameters.AddWithValue("@Quantity", request.Quantity);
        try
        {
            _sqlConnection.Open();
            await command.ExecuteNonQueryAsync(cancellationToken);
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