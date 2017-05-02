using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPBaseRepo.Controllers;
using NPBaseRepo.Models;
using NPBaseRepo.Models.Entities;
using NPBaseRepo.Repository;
using NPBaseRepo.Tests.Fake;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NPBaseRepo.Tests.Controllers
{
    [TestClass]
    public class MovieControllerTests
    {
        private IDataRepository _db;
        private MoviesController _controller;

        private IDataRepository GetRepository()
        {
            if (ConfigurationManager.AppSettings["UseDatabase"].ToLower() == "true")
            {
                return new DataRepository();
            }

            return new FakeDataRepository();
        }

        [TestInitialize]
        public void Initialize()
        {
            _db = GetRepository();
            _db.ResetDatabase();
            _controller = new MoviesController(_db);

            var director = new Director();
            director.Id = 1;
            director.Name = "Test Director";

            _db.Add(director);

            var movie = new Movie();
            movie.Id = 1;
            movie.Name = "Test Movie 1";
            movie.Description = "Test Descr 1";
            movie.DirectorId = director.Id;
            movie.Director = director;

            _db.Add(movie);

            movie = new Movie();
            movie.Id = 2;
            movie.Name = "Test Movie 2";
            movie.Description = "Test Descr 2";
            movie.DirectorId = director.Id;
            movie.Director = director;

            _db.Add(movie);
            _db.Save();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        [TestMethod]
        public void Index_CorrectView()
        {
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName.ToLower() == "index");
        }

        [TestMethod]
        public void Index_CorrectResults()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var models = result.Model as IEnumerable<Movie>;

            Assert.AreEqual(2, models.Count());
            Assert.IsTrue(models.Any(a => a.Name == "Test Movie 1"));
            Assert.IsTrue(models.Any(a => a.Name == "Test Movie 2"));
        }

        [TestMethod]
        public void Details_MovieFound_CorrectView()
        {
            var movie = _db.GetMovies().FirstOrDefault();
            var result = _controller.Details(movie.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName.ToLower() == "details");
        }

        [TestMethod]
        public void Details_MovieFound_CorrectModel()
        {
            var movie = _db.GetMovies().FirstOrDefault();
            var result = _controller.Details(movie.Id) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as Movie;
            Assert.IsNotNull(model);
            Assert.AreEqual("Test Movie 1", model.Name);
            Assert.AreEqual("Test Descr 1", model.Description);
            Assert.AreEqual("Test Director", model.Director.Name);
        }

        [TestMethod]
        public void Details_MovieNotFound()
        {
            var result = _controller.Details(-1) as HttpNotFoundResult;
            Assert.IsNotNull(result);
        }
    }
}
