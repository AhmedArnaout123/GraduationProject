using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.CommandsHandlers.AddProductToWishList;

public class AddProductToWishListCommandHandler : IRequestHandler<AddProductToWishListCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public AddProductToWishListCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(AddProductToWishListCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Products_WishLists Values(@CustomerId,@ProductId)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
        command.Parameters.AddWithValue("@ProductId", request.ProductId);
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