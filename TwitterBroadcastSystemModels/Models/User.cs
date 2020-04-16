using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterBroadcastSystemModel.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public string UserName { get; set; }
        [MinLength(1)]
        public string UserID { get; set; }
    }
}
