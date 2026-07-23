using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.data.Results;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class CartServices : ICartService
    {
        #region Fields
        private readonly ICartRepository _CartRepository;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly IProductRepository _ProductRepository;
        #endregion

        #region Constructor
        public CartServices(ICartItemRepository cartItemRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _CartItemRepository = cartItemRepository;
            _CartRepository = cartRepository;
            _ProductRepository = productRepository;
        }
        #endregion
        public async Task<string> AddCartAsync(Cart cart)
        {
            await _CartRepository.AddAsync(cart);
            return ResultString.Success;
        }

        public async Task<string> AddCartItemAsync(AddCartItemResult cartItem)
        {
            var cart = await _CartRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == cartItem.UserId);
            if (cart == null)
            {
                return "User Cart Not Found";
            }

            var product = await _ProductRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                return "Product Not Found";
            }

            if (product.Stock < cartItem.Quantity)
            {
                return "Cart Quantity greater than Product Stock";
            }

            if (product.Stock == 0)
            {
                return "Product Stock Is Empty";
            }

            var NewCartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Total = product.Price * cartItem.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await _CartItemRepository.AddAsync(NewCartItem);

            return ResultString.Success;

        }
        public async Task<string> UpdateCartItemAsync(UpdateCartItemResult cartItem)
        {
            var UpdatedCarItem = await _CartItemRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.CartItemId == cartItem.CartItemId);

            if (UpdatedCarItem == null)
            {
                return "Cart Item Not Found";
            }

            var product = await _ProductRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                return "Product Not Found";
            }

            if (product.Stock < cartItem.Quantity)
            {
                return "Cart Quantity greater than Product Stock";
            }

            if (product.Stock == 0)
            {
                return "Product Stock Is Empty";
            }

            UpdatedCarItem.Total = product.Price * cartItem.Quantity;

            await _CartItemRepository.UpdateAsync(UpdatedCarItem);

            return ResultString.Success;
        }
        public async Task<string> DeleteCartItemAsync(int CartItemId)
        {
            var DeletedCarItem = await _CartItemRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.CartItemId == CartItemId);

            if (DeletedCarItem == null)
            {
                return "Cart Item Not Found";
            }

            var trans = await _CartItemRepository.BeginTransactionAsync();
            try
            {
                await _CartItemRepository.DeleteAsync(DeletedCarItem);
                await trans.CommitAsync();
                return ResultString.Success;
            }
            catch
            {
                await trans.RollbackAsync();
                return ResultString.Failure;
            }

        }

        public async Task<ICollection<CartItem>> GetUserCartItemsAsync(int UserId)
        {

            var cart = await _CartRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == UserId);

            if (cart == null)
            {
                return null;
            }
            return await _CartItemRepository.GetTableNoTracking()
                            .Include(x => x.Product).ThenInclude(x => x.Brand)
                            .Include(x => x.Product).ThenInclude(x => x.Category)
                            .Include(x => x.Product).ThenInclude(x => x.ProductImages)
                            .Where(x => x.CartId == cart.CartId).ToListAsync();
        }

    }
}
