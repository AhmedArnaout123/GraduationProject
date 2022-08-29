using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Discounts.Commands.CreateDiscount;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Discounts.CommandHandlers;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateDiscountCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        
        string stmt = "INSERT INTO Discounts Values(@Id, @Percentage, @Description)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@Id", request.Id);
        command.Parameters.AddWithValue("@Percentage", request.Percentage);
        command.Parameters.AddWithValue("@Description", request.Description);
        try
        {
            _sqlConnection.Open();
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result.IsSuccess = false;
        }
        finally
        {
            _sqlConnection.Close();
        }

        return result;
    }
}