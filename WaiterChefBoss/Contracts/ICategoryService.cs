using WaiterChefBoss.Services.Category;

namespace WaiterChefBoss.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CategoryExists(int id);
        Task<IEnumerable<CategoryViewModelService>> AllCategories();

        Task<CategoryViewModelService> CategoryDetails(int id);
    }
}
