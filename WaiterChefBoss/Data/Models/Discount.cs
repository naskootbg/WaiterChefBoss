using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public double Total { get; set; }

        public int Percent { get; set; }
    }
}
