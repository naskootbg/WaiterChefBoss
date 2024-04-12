using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CategoryExists(int id);
        Task<List<CategoryViewModelService>> AllActiveCategories();
        Task<List<CategoryViewModelService>> AllCategories();
        Task<List<CategoryViewModelService>> BarmanCategories();
        Task<CategoryViewModelService> CategoryDetails(int id);
    }
}
