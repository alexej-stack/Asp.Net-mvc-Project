using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class VoteLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }
        public int VoteForId { get; set; }
        public int SectionId { get; set; }
        public string UserName { get; set; }
        public Int16 Vote { get; set; }
        public bool Active { get; set; }
    }
}