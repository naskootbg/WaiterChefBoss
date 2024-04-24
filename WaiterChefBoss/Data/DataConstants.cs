using System.Globalization;

namespace WaiterChefBoss.Data
{
    public class DataConstants
    {
        public const int MinTitleLenght = 3;
        public const int MaxTitleLenght = 100;
        public const int MinDescriptionLenght = 3;
        public const int MaxDescriptionLenght = 1500;
        public const string ChefRole = "Chef";
        public const string WaiterRole = "Waiter";
        public const string BossRole = "Boss";
        public const string BarmanRole = "Barman";
        public const int TablesInTheRestaurant = 10;
        public const int NumberProductsHomePage = 2;
        public const int NumberProductsCategoryPage = 2;
        public const int MaxIngredientLenght = 1000;

        public const string ProductMemoryCacheKey = "ProductMemoryCacheKey";
        public const string CategoryMemoryCacheKey = "CategoryMemoryCacheKey";

    }
}