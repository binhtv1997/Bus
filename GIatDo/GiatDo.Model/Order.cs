using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GiatDo.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? TakeTime { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Shipper ShipperTake { get; set; }
        public Guid? ShipperTakeId { get; set; }
        public virtual Shipper ShipperDeliver { get; set; }
        public Guid? ShipperDeliverId { get; set; }
        public virtual Slot SlotTake { get; set; }
        public Guid? SlotTakeId { get; set; }
        public virtual Slot SlotDelivery { get; set; }
        public Guid? SlotDeliveryId { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDelete { get; set; }
    }
}
