using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IBasketService
    {
        public Task AddProduct();

        public Task RemoveProduct(int id);

        public Task Total(List<ProductViewModel> products);
    }
}
