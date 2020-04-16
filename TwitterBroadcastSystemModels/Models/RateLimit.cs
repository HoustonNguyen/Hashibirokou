using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterBroadcastSystemModel.Models
{
    public class RateLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public int Allowance { get; set; }
        public int Remaining { get; set; }
        public DateTime LastChecked { get; set; }
        public DateTime AllowanceReset { get; set; }
    }
}
