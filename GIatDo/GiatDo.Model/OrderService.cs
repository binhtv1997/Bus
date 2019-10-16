using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class OrderService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public float Price { get; set; }
        public virtual Order Order { get; set; }
        public Guid? OrderId { get; set; }
        public virtual Services Service { get; set; }
        public Guid? ServiceId { get; set; }
        public bool IsDelete { get; set; }
    }
}
