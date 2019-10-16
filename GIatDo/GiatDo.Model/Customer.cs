using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Rate { get; set; }
        public virtual Account Account { get; set; }
        public Guid? AccountId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDelete { get; set; }

    }
}
