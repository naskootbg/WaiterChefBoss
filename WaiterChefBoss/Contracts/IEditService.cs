using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IEditService
    {
        Task AddProduct(ProductFormViewModel product);

        Task AddCategory(CategoryViewModelService category);

        Task<ProductFormViewModel> EditProduct(ProductFormViewModel? product, int productId);
        Task<CategoryViewModelService> EditCategory(CategoryViewModelService category, int categoryId);

        Task DeleteProduct(int productId);
        Task DeleteCategory(int categoryId);
    }
}
