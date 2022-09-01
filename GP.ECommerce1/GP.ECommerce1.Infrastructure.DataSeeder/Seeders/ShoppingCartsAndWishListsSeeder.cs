// using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToShoppingCart;
// using GP.ECommerce1.Core.Application.Customers.Commands.AddProductToWishList;
// using GP.Utilix;
// using MediatR;
//
// namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
//
// public class ShoppingCartsAndWishListsSeeder
// {
//     private readonly IMediator _mediator;
//     private readonly DataSeedingHelper _dataSeedingHelper;
//
//     public ShoppingCartsAndWishListsSeeder(IMediator mediator, DataSeedingHelper dataSeedingHelper)
//     {
//         _mediator = mediator;
//         _dataSeedingHelper = dataSeedingHelper;
//     }
//
//     public async Task Seed()
//     {
//         Console.WriteLine("Getting Ready to Seed Cart and Wish Lists.");
//         var customers = await _dataSeedingHelper.GetAllCustomers();
//         var products = await _dataSeedingHelper.GetAllProducts();
//         Console.WriteLine("Seeding Shopping Carts and Wish Lists Started.");
//         for(var k = 0; k < customers.Count; k++) 
//         {
//             for (int i = 0; i < Randoms.RandomInt(4); i++)
//             {
//                 var index = Randoms.RandomInt(products.Count);
//                 var command = new AddProductToShoppingCartCommand
//                 {
//                     Quantity = Randoms.RandomQuantity(),
//                     CustomerId = customers[k].Id,
//                     ProductId = products[index].Id,
//                     ProductName = products[index].Name,
//                     ProductPrice = products[index].Price,
//                 };
//                 await _mediator.Send(command);
//             }
//             for (int i = 0; i < Randoms.RandomInt(4); i++)
//             {
//                 var index = Randoms.RandomInt(products.Count);
//                 var command = new AddProductToWishListCommand
//                 {
//                     CustomerId = customers[k].Id,
//                     ProductId = products[index].Id,
//                     ProductName = products[index].Name,
//                     ProductPrice = products[index].Price,
//                 };
//                 await _mediator.Send(command);
//             }
//             Console.WriteLine($"customer: {k}");
//         }
//     }
// }