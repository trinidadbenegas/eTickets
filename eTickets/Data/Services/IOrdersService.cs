using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrdersAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress);

        Task <List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
