using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NPBaseRepo;
using NPBaseRepo.Models.Entities;
using NPBaseRepo.Repository;
using NPBaseRepo.Models;

namespace NPBaseRepo.Controllers
{
    public class MoviesController : Controller
    {
        private IDataRepository _db;

        // GET: Movies
        public ActionResult Index()
        {
            var models = _db.GetMoviesWithDirectors().ToList();

            return View(models);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var model = _db.GetMovie(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie();
                movie.Name = model.Name;
                movie.Description = model.Description;

                var director = _db.GetDirectors().FirstOrDefault(f => f.Name == model.DirectorName);
                if (director == null)
                {
                    director = new Director();
                    director.Name = model.DirectorName;
                    movie.Director = director;
                }
                else
                {
                    movie.DirectorId = director.Id;
                }

                _db.Add(movie);
                _db.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _db.GetMovie(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(new MovieModel(model));
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieModel model)
        {
            var movie = _db.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                movie.Name = model.Name;
                movie.Description = model.Description;

                var director = _db.GetDirectors().FirstOrDefault(f => f.Name == model.DirectorName);
                if (director == null)
                {
                    director = new Director();
                    director.Name = model.DirectorName;
                    movie.Director = director;
                }
                else
                {
                    movie.DirectorId = director.Id;
                }

                _db.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = _db.GetMovie(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var movie = _db.GetMovie(id);
            if (movie != null)
            {
                _db.Delete(movie);
                _db.Save();
            }

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            _db.Dispose();

            base.Dispose(disposing);
        }

        public MoviesController(IDataRepository db)
        {
            _db = db;
        }
    }
}
