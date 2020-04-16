using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBroadcastSystemModel.Models
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }

        public string Description { get; set; }

        public string Webhook { get; set; }

        public virtual DestinationType DestinationType { get; set; }
    }
}
