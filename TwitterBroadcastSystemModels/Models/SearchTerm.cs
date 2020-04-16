using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterBroadcastSystemModel.Models
{
    public class SearchTerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public string CompareType { get; set; } = Constants.CompareType.CONTAINS;
        [MinLength(1)]
        public string Text { get; set; }
    }
}
