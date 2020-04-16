using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterBroadcastSystemModel.Models
{
    public class Procedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public string Description { get; set; }
        public virtual ProcedureCategory ProcedureCategory { get; set; }
        public virtual RateLimit RateLimit { get; set; }
    }
}
