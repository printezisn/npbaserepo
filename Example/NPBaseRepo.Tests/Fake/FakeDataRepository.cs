using NPBaseRepo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPBaseRepo.Models.Entities;

namespace NPBaseRepo.Tests.Fake
{
    public class FakeDataRepository : FakeDataRepositoryBase, IDataRepository
    {
        public IQueryable<Movie> GetMoviesWithDirectors()
        {
            return this.Movies.AsQueryable();
        }
    }
}
