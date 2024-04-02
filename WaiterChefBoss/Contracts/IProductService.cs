﻿using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IProductService
    { 
        Task<IEnumerable<ProductViewService>> AllProducts();
        Task<IEnumerable<ProductViewService>> AllProductsPerCategory(int categoryId);
        Task<bool> ProductExists(int id);
        Task<ProductViewService> ProductById(int id);        
        Task AddToCart(string userId, int productId);
        Task<int> BlankOrder(string userId);        
        Task<bool> IsBlankOrder(string userId);
        Task<IEnumerable<ProductViewService>> ProductsInTheOrder(string userId);
    }
}
