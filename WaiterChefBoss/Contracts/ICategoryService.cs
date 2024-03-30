using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CategoryExists(int id);
        Task<List<CategoryViewModelService>> AllCategories();
        Task<CategoryViewModelService> CategoryDetails(int id);
    }
}
