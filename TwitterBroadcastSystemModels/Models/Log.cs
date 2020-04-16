using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterBroadcastSystemModel.Models
{
    public class Log
    {
        public enum LogLevels
        {
            Information = 0,
            Error = 1,
            Warning = 2
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PrimaryKey { get; set; }
        public DateTime LogDateTime { get; set; }
        public int LogLevel { get; set; }
        public string LogMessage { get; set; }
        public string LogMessageDetail { get; set; }
    }
}
