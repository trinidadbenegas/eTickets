using eTickets.Data.Services;
using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using eTickets.Data.ViewModels;
using eTickets.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {

        private readonly IMoviesService _moviesService;

        private readonly ShoppingCart _shoppingCart;

        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View(orders);
        }

       
        public IActionResult ShoppingCart()
        {
            var items= _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems= items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };



            return View(response);
        }

        public async Task <IActionResult> AddToShoppingCart( int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null) { 

            _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {

                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

           await _ordersService.StoreOrdersAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View( "OrderCompleted");
        }
    }
}
