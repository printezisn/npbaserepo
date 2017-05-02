using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NPBaseRepo.Models.Entities
{
    public partial class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [DisplayName("Director")]
        public int DirectorId { get; set; }


        public Director Director { get; set; }
    }
}