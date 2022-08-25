using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Commands.AddAddress;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.CommandsHandlers.AddAddress;

public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public AddAddressCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(AddAddressCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Addresses Values(@Id,@Country,@State,@City,@Street1,@Street2,@CustomerId)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@Id", request.Id);
        command.Parameters.AddWithValue("@Country", request.Country);
        command.Parameters.AddWithValue("@State", request.State);
        command.Parameters.AddWithValue("@City", request.City);
        command.Parameters.AddWithValue("@Street1", request.Street1);
        command.Parameters.AddWithValue("@Street2", request.Street2);
        command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
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