using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Reviews.Commands.CreateReview;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Reviews.CommandsHandlers.CreateReviewCommandHandler;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateReviewCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Reviews Values(@Id,@Rate,@Comment,@CustomerId,@ProductId)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@Id", request.Id);
        command.Parameters.AddWithValue("@Rate", request.Rate);
        command.Parameters.AddWithValue("@Comment", request.Comment);
        command.Parameters.AddWithValue("@ProductId", request.ProductId);
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