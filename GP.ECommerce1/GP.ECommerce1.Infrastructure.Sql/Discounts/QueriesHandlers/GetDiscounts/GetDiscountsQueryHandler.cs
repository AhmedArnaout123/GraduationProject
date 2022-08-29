using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Discounts.Queries.GetDiscounts;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Discounts.QueriesHandlers.GetDiscounts;

public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, Result<List<Discount>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetDiscountsQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Discount>>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Discount>> {IsSuccess = true};
        List<Discount> discounts = new();
        string stmt = "SELECT * FROM Discounts";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                var discount = new Discount
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    Percentage = Convert.ToInt32(reader["Percentage"]),
                    Description = Convert.ToString(reader["Description"])!,
                };
                discounts.Add(discount);
            }

            result.Value = discounts;
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