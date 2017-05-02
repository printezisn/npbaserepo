using NPBaseRepo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NPBaseRepo.Repository
{
    public class DataRepository : DataRepositoryBase, IDataRepository
    {
        public override Movie GetMovie(int id)
        {
            return Db.Movies.Include(i => i.Director).FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<Movie> GetMoviesWithDirectors()
        {
            return Db.Movies.Include(i => i.Director);
        }
    }
}