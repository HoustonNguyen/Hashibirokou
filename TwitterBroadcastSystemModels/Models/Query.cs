using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBroadcastSystemModel.Models
{
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public bool ShouldTranslate { get; set; } = false;
        public bool IncludesImages { get; set; } = false;

        public virtual Procedure Procedure { get; set; }
        public virtual ICollection<SearchTerm> SearchTerms { get; set; }
        public virtual ICollection<QueryTargetUser> TargetedUsers { get; set; }
    }
}
