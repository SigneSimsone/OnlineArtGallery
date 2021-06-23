using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class ArtworkModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public bool Availability { get; set; }
        public string FilePath { get; set; }

        public ArtistModel Artist { get; set; }
        public StyleModel Style { get; set; }

        public ArtworkModel()
        {
            Id = Guid.NewGuid();
        }

        public ICollection<UserModel> Users { get; set; }
        public ICollection<FeedbackModel> Feedbacks { get; set; }
        public ICollection<ExhibitionModel> Exhibitions { get; set; }
        public ICollection<OrderModel> Orders { get; set; }

    }
}
