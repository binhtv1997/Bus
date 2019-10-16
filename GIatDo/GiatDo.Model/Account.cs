using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string User_Id { get; set; }
        public virtual Store Store { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDelete { get; set; }
    }
}
