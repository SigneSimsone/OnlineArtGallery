using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Models
{
    public class OrderModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public OrderModel()
        {
            Id = Guid.NewGuid();
        }

        public ArtworkModel Artwork { get; set; }
        public UserModel User { get; set; }
    }
}
