using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Commands.CreateCustomer;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.CommandsHandlers.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateCustomerCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Customers Values(@Id,@FirstName,@LastName,@PhoneNumber,@Email,@PasswordHash)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@Id", request.Id);
        command.Parameters.AddWithValue("@FirstName", request.FirstName);
        command.Parameters.AddWithValue("@LastName", request.LastName);
        command.Parameters.AddWithValue("@PhoneNumber", request.PhoneNumber);
        command.Parameters.AddWithValue("@Email", request.Email);
        command.Parameters.AddWithValue("@PasswordHash", request.PasswordHash);
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