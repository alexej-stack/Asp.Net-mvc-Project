using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task2.Models
{

   
        public class CompanyComment
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CommentId { get; set; }

            public string Comments { get; set; }

            public DateTime? ThisDateTime { get; set; }

            public int CompanyId { get; set; }

            public int? Rating { get; set; }
        }
    }
