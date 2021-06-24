using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineArtGallery.Web.Models
{
    public class AuditModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
        public UserModel User { get; set; }

        public AuditModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
