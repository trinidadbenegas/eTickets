using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n=> n.User).ToListAsync();

            if( userRole != "Admin")
            {
                orders= orders.Where(n=> n.UserId == userId).ToList();
            }
            return orders;


        }

        public async Task StoreOrdersAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = UserEmailAddress

            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            //de esta forma ya se genera una order con su id en la bd

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price,

                };

               await _context.OrderItems.AddAsync(orderItem);
               
            }
            await _context.SaveChangesAsync();
        }
    }
}
