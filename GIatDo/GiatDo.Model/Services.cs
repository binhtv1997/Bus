using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class Services
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public Guid ServiceTypeId { get; set; }
        public virtual Store Store { get; set; }
        public Guid StoreId { get; set; }
        public bool IsDelete { get; set; }
        public string Imgurl { get; set; }
    }
}
