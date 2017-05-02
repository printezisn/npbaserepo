using NPBaseRepo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPBaseRepo.Repository
{
    public partial interface IDataRepository
    {
        IQueryable<Movie> GetMoviesWithDirectors();
    }
}