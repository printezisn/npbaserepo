

using System;
using System.Linq;
using NPBaseRepo.Models.Entities;
using System.Collections.Generic;

namespace NPBaseRepo.Repository
{
	public partial interface IDataRepository : IDisposable
	{
		/// <summary>
        /// Saves the current changes
        /// </summary>
		void Save();

				#region Directors

		/// <summary>
        /// Returns a query for Directors
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		IQueryable<Director> GetDirectors(bool includeTracking = false);
		
		/// <summary>
        /// Returns a search query for Directors
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		IQueryable<Director> SearchDirectors(string searchTerm);
		
		/// <summary>
        /// Returns a Director entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		Director GetDirector(int id);

		/// <summary>
        /// Adds a new Director entity
        /// </summary>
        /// <param name="model">The Director entity to add</param>
		void Add(Director model);

		/// <summary>
        /// Deletes a Director entity
        /// </summary>
        /// <param name="model">The Director entity to delete</param>
		void Delete(Director model);

		#endregion

				#region Movies

		/// <summary>
        /// Returns a query for Movies
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		IQueryable<Movie> GetMovies(bool includeTracking = false);
		
		/// <summary>
        /// Returns a search query for Movies
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		IQueryable<Movie> SearchMovies(string searchTerm);
		
		/// <summary>
        /// Returns a Movie entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		Movie GetMovie(int id);

		/// <summary>
        /// Adds a new Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to add</param>
		void Add(Movie model);

		/// <summary>
        /// Deletes a Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to delete</param>
		void Delete(Movie model);

		#endregion

		
		/// <summary>
        /// Deletes all the data from the database. CAUTION: Only used for integration tests
        /// </summary>
        void ResetDatabase();
	}

	public abstract class DataRepositoryBase
	{
		private MovieContext _db;
        protected MovieContext Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new MovieContext();
                    _db.Configuration.ValidateOnSaveEnabled = false;
                }

                return _db;
            }
        }

		/// <summary>
        /// Disposes the repository
        /// </summary>
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

		/// <summary>
        /// Saves the current changes
        /// </summary>
        public void Save()
        {
            if (_db != null)
            {
                _db.SaveChanges();
            }
        }

		/// <summary>
        /// Deletes all the data from the database. CAUTION: Only used for integration tests
        /// </summary>
        public void ResetDatabase()
		{
					Db.Directors.RemoveRange(Db.Directors.ToList());
					Db.Movies.RemoveRange(Db.Movies.ToList());
					Save();
		}

				#region Directors

		/// <summary>
        /// Returns a query for Directors
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Director> GetDirectors(bool includeTracking = false)
		{
						if (includeTracking)
            {
                return Db.Directors;
            }

            return Db.Directors.AsNoTracking();
					}

		
		/// <summary>
        /// Applies a search query, for Directors, to an existing query
        /// </summary>
		/// <param name="query">The query to apply the search to</param>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		protected virtual IQueryable<Director> SearchDirectorsQuery(IQueryable<Director> query, string searchTerm)
		{
			return query.Where(w => (w.Name != null && w.Name.Contains(searchTerm)));
		}

		/// <summary>
        /// Returns a search query for Directors
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Director> SearchDirectors(string searchTerm)
		{
			return SearchDirectorsQuery(GetDirectors(), searchTerm);
		}

		
		/// <summary>
        /// Returns a Director entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		public virtual Director GetDirector(int id)
		{
					return Db.Directors.Find(id);
				}

		/// <summary>
        /// Adds a new Director entity
        /// </summary>
        /// <param name="model">The Director entity to add</param>
		public virtual void Add(Director model)
		{
			Db.Directors.Add(model);
		}

		/// <summary>
        /// Deletes a Director entity
        /// </summary>
        /// <param name="model">The Director entity to delete</param>
		public virtual void Delete(Director model)
		{
					Db.Directors.Remove(model);
				}

		#endregion

				#region Movies

		/// <summary>
        /// Returns a query for Movies
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Movie> GetMovies(bool includeTracking = false)
		{
						if (includeTracking)
            {
                return Db.Movies;
            }

            return Db.Movies.AsNoTracking();
					}

		
		/// <summary>
        /// Applies a search query, for Movies, to an existing query
        /// </summary>
		/// <param name="query">The query to apply the search to</param>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		protected virtual IQueryable<Movie> SearchMoviesQuery(IQueryable<Movie> query, string searchTerm)
		{
			return query.Where(w => (w.Name != null && w.Name.Contains(searchTerm)) || (w.Description != null && w.Description.Contains(searchTerm)));
		}

		/// <summary>
        /// Returns a search query for Movies
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Movie> SearchMovies(string searchTerm)
		{
			return SearchMoviesQuery(GetMovies(), searchTerm);
		}

		
		/// <summary>
        /// Returns a Movie entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		public virtual Movie GetMovie(int id)
		{
					return Db.Movies.Find(id);
				}

		/// <summary>
        /// Adds a new Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to add</param>
		public virtual void Add(Movie model)
		{
			Db.Movies.Add(model);
		}

		/// <summary>
        /// Deletes a Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to delete</param>
		public virtual void Delete(Movie model)
		{
					Db.Movies.Remove(model);
				}

		#endregion

			}

	public abstract class FakeDataRepositoryBase
	{
		/// <summary>
        /// Saves the current changes
        /// </summary>
		public void Save()
        {

        }

		/// <summary>
        /// Disposes the repository
        /// </summary>
        public void Dispose()
        {

        }

		/// <summary>
        /// Deletes all the data from the database. CAUTION: Only used for integration tests
        /// </summary>
        public void ResetDatabase()
		{

		}

				#region Directors

		protected List<Director> Directors = new List<Director>();

		/// <summary>
        /// Returns a query for Directors
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Director> GetDirectors(bool includeTracking = false)
		{
			return this.Directors.AsQueryable();
		}

		
		/// <summary>
        /// Returns a search query for Directors
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Director> SearchDirectors(string searchTerm)
		{
			return this.Directors.Where(w => (w.Name != null && w.Name.Contains(searchTerm))).AsQueryable();
		}

		
		/// <summary>
        /// Returns a Director entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		public virtual Director GetDirector(int id)
		{
			return this.Directors.FirstOrDefault(f => f.Id == id);
		}

		/// <summary>
        /// Adds a new Director entity
        /// </summary>
        /// <param name="model">The Director entity to add</param>
		public virtual void Add(Director model)
		{
			this.Directors.Add(model);
		}

		/// <summary>
        /// Deletes a Director entity
        /// </summary>
        /// <param name="model">The Director entity to delete</param>
		public virtual void Delete(Director model)
		{
			this.Directors.Remove(model);
		}

		#endregion

				#region Movies

		protected List<Movie> Movies = new List<Movie>();

		/// <summary>
        /// Returns a query for Movies
        /// </summary>
        /// <param name="includeTracking">Indicates if the returned entities must be tracked</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Movie> GetMovies(bool includeTracking = false)
		{
			return this.Movies.AsQueryable();
		}

		
		/// <summary>
        /// Returns a search query for Movies
        /// </summary>
        /// <param name="searchTerm">The term to search for</param>
        /// <returns>The query</returns>
		public virtual IQueryable<Movie> SearchMovies(string searchTerm)
		{
			return this.Movies.Where(w => (w.Name != null && w.Name.Contains(searchTerm)) || (w.Description != null && w.Description.Contains(searchTerm))).AsQueryable();
		}

		
		/// <summary>
        /// Returns a Movie entity
        /// </summary>
        /// <param name="id">The primary key of the entity</param>
        /// <returns>The entity or null if it was not found</returns>
		public virtual Movie GetMovie(int id)
		{
			return this.Movies.FirstOrDefault(f => f.Id == id);
		}

		/// <summary>
        /// Adds a new Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to add</param>
		public virtual void Add(Movie model)
		{
			this.Movies.Add(model);
		}

		/// <summary>
        /// Deletes a Movie entity
        /// </summary>
        /// <param name="model">The Movie entity to delete</param>
		public virtual void Delete(Movie model)
		{
			this.Movies.Remove(model);
		}

		#endregion

			}
}

