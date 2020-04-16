using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBroadcastSystemModel.Models
{
    public class QueryTargetUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public virtual Query Query { get; set; }
        public virtual User User { get; set; }
    }
}
