﻿using WaiterChefBoss.Services.Category;

namespace WaiterChefBoss.Services.Product
{
    public class ProductViewService
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal TimeCooking { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Calories { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
