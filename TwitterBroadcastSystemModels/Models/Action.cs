using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBroadcastSystemModel.Models
{
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public string Description { get; set; }
        public DateTime? LastTimeChecked { get; set; }
        public string LastFoundPostID { get; set; }
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
        public Boolean Active { get; set; } = false;

        public virtual ICollection<Destination> Destinations { get; set; }
        public virtual ICollection<Query> Queries { get; set; }
        public virtual User Owner { get; set; }
        public virtual PriorityLevel PriorityLevel { get; set; }
    }
}
