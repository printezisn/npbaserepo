using NPBaseRepo.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NPBaseRepo.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [DisplayName("Director")]
        [Required]
        [MaxLength(100)]
        public string DirectorName { get; set; }

        public MovieModel()
        {

        }

        public MovieModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.DirectorName = movie.Director.Name;
        }
    }
}