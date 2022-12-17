using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class WebApiContext : DbContext
    {
        //public WebApiContext(DbContextOptions<WebApiContext> options)
        //    : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebApi.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<WebApi.Models.Users> Users { get; set; } = default!;
    }
}
