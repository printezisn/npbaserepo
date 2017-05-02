using NPBaseRepo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NPBaseRepo
{
    public class MovieContext : DbContext
    {
        public MovieContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}