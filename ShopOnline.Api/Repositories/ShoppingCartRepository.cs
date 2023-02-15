using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly ShopOnlineDbContext shopOnlineDbContext;

		public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
		{
			this.shopOnlineDbContext = shopOnlineDbContext;
		}

		private async Task<bool> CartItemExists(int cartId, int productId)
		{
			return await this.shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
		}
		public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
		{
			if(await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false) {
				var item = await (from product in this.shopOnlineDbContext.Products
													where product.Id == cartItemToAddDto.ProductId
													select new CartItem
													{
														CartId = cartItemToAddDto.CartId,
														ProductId = product.Id,
														Qty = cartItemToAddDto.Qty,
													}).SingleOrDefaultAsync();
				if (item != null)
				{
					var result = await this.shopOnlineDbContext.CartItems.AddAsync(item);
					await this.shopOnlineDbContext.SaveChangesAsync();
					return result.Entity;
				}
			}
			
			return null;
		}

		public Task<CartItem> DeleteItem(int id)
		{
			throw new NotImplementedException();
		}

		public Task<CartItem> GetItem(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CartItem>> GetItems(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
