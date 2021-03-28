using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WebsiteShoe.Entities;

namespace WebsiteShoe.Models
{
    public class Cart
    {
        private readonly ShoeDbContext _dbContext;

        public int ShoeId { get; set; }
        public string ShoeName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Images { get; set; }
        public double TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
            set { }
        }

        public Cart(ShoeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart()
        {
        }
    }
}
