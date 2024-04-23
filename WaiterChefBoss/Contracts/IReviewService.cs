using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IReviewService
    {
        Task<int> Add(ReviewViewModel model, string userId);

        Task<ReviewViewModel> Edit(int id, ReviewViewModel? model);

        Task Delete(int id);

        Task<bool> ReviewIsFromTheUser(int id, string userId);

        Task<ReviewViewModel> Show(int id);

        Task<IEnumerable<ReviewViewModel>> All();

        Task<IEnumerable<ReviewViewModel>> MyReviews(string userId);

        Task<IEnumerable<ReviewViewModel>> ProductReviews(int id, double average, int total);
         

        Task<double> AverageScore(int productId);
        Task<int> ProductReviewsCount(int id);

    }
}
