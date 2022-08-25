using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Query.CustomerLogin;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.QueriesHandler.CustomerLogin;

public class CustomerLoginQueryHandler : IRequestHandler<CustomerLoginQuery, Result<List<Category>>>
{
    private readonly SqlConnection _sqlConnection;

    public CustomerLoginQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Category>>> Handle(CustomerLoginQuery request, CancellationToken cancellationToken)
    {
        return new Result<List<Category>> {Value = new List<Category>()};
    }
}