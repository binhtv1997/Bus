using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class Shipper
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
        public virtual ICollection<Order> OrderTakes { get; set; }
        public virtual ICollection<Order> OrderDelivery { get; set; }
        public bool IsDelete { get; set; }
    }
}
