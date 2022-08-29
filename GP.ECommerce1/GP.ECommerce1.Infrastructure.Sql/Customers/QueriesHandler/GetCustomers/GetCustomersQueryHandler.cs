using System.Data.SqlClient;
using GP.ECommerce1.Core.Application.Customers.Query.GetCustomers;
using GP.ECommerce1.Core.Domain;
using GP.Utilix;
using MediatR;

namespace GP.ECommerce1.Infrastructure.Sql.Customers.QueriesHandler.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<List<Customer>>>
{
    private readonly SqlConnection _sqlConnection;

    public GetCustomersQueryHandler(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<Result<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<List<Customer>> {IsSuccess = true};
        List<Customer> customers = new();
        string stmt = "SELECT * FROM Customers";
        var command = new SqlCommand(stmt, _sqlConnection);
        try
        {
            _sqlConnection.Open();
            var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (reader.Read())
            {
                List<Address> addresses = new();
                var customer = new Customer
                {
                    Id = Guid.Parse(Convert.ToString(reader["Id"])!),
                    FirstName = Convert.ToString(reader["FirstName"])!,
                    LastName = Convert.ToString(reader["LastName"])!,
                    Email = Convert.ToString(reader["Email"])!,
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"])!,
                };
                var stmt2 = "SELECT * FROM Addresses WHERE CustomerId=@CustomerId";
                var command2 = new SqlCommand(stmt2, _sqlConnection);
                command2.Parameters.AddWithValue("CustomerId", customer.Id);
                var reader2 = await command2.ExecuteReaderAsync();
                while (reader2.Read())
                {
                    var address = new Address()
                    {
                        City = Convert.ToString(reader2["City"])!,
                        Country = Convert.ToString(reader2["Country"])!,
                        State = Convert.ToString(reader2["State"])!,
                        Street1 = Convert.ToString(reader2["Street1"])!,
                        Street2 = Convert.ToString(reader2["Street2"])!,
                        Id = Guid.Parse(Convert.ToString(reader2["Id"])!),
                        CustomerId = customer.Id
                    };
                    customer.Addresses.Add(address);
                }
                customers.Add(customer);
            }

            result.Value = customers;
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