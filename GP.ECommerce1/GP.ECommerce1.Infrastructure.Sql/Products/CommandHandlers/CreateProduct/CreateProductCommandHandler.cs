using System.Data;
using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Products.Commands.CreateProduct;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Products.CommandHandlers.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly SqlConnection _sqlConnection;

    public CreateProductCommandHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = new Result{IsSuccess = true};
        _sqlConnection.Open();
        var transaction = _sqlConnection.BeginTransaction();
        var command = _sqlConnection.CreateCommand();
        
        try
        {

            command.Transaction = transaction;
            command.CommandText = "INSERT INTO Products Values(@Id,@Name,@Description,@MainImageUri,@Price,@CategoryId,@DiscountId,'0')";
            
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Name", request.Name);
            command.Parameters.AddWithValue("@Description", request.Description);
            command.Parameters.AddWithValue("@MainImageUri", request.MainImageUri);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
            command.Parameters.AddWithValue("@DiscountId", (object) request.DiscountId! ?? DBNull.Value);
            await command.ExecuteNonQueryAsync();

            command.Parameters.Clear();
            command.CommandText = "INSERT INTO ProductImages Values(@Id, @Uri, @ProductId)";
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@Uri", SqlDbType.NVarChar);
            command.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            foreach (var uri in request.Images)
            {
                command.Parameters["@Id"].Value = Guid.NewGuid();
                command.Parameters["@Uri"].Value = uri;
                command.Parameters["@ProductId"].Value = request.Id;
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