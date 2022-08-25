using System.Data;
using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Categories.CommandsHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateCategoryCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        string stmt = "INSERT INTO Categories Values(@Id,@Name,@ParentId)";
        var command = new SqlCommand(stmt, _sqlConnection);
        command.Parameters.AddWithValue("@Id", request.Id);
        command.Parameters.AddWithValue("@Name", request.Name);
        command.Parameters.AddWithValue("@ParentId", (object) request.ParentId ?? DBNull.Value);
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