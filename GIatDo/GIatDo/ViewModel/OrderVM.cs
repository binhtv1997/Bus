using System;
using System.Collections.Generic;

namespace GIatDo.ViewModel
{
    public class OrderTakeVM
    {
        public Guid Id { get; set; }
        public float TotalPrice { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ShipperTakeId { get; set; }
        public Guid? SlotTakeId { get; set; }
    }

    public class OrderDeleveryVM
    {
        public Guid Id { get; set; }
        public float TotalPrice { get; set; }
        public bool Status { get; set; }
        public Guid? ShipperDeliverId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid? SlotDeliveryId { get; set; }
    }
    public class OrderCM
    {
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? TakeTime { get; set; }
        public List<ListOrderServiceCM> ListOrderServices { get; set; }
    }
    public class ListOrderServiceCM
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public Guid? ServiceId { get; set; }
    }
    public class OrderVM
    {
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? TakeTime { get; set; }
        public float TotalPrice { get; set; }   
        public String Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ShipperTakeId { get; set; }
        public Guid? ShipperDeliverId { get; set; }
        public Guid? SlotTakeId { get; set; }
        public Guid? SlotDeliveryId { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class UpdateTakeDelivery
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
    }
    public class UpdateShipperDelivery
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
    }
    public class UpdateOrderStatus
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
    }
    public class UpdateSlotDelivery
    {
        public Guid SlotId { get; set; }
        public Guid OrderId { get; set; }
    }
    public class UpdateShipperTake
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
    }

    public class OrderAVM
    {
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? TakeTime { get; set; }
        public float TotalPrice { get; set; }
        public String Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ShipperTakeId { get; set; }
        public Guid? ShipperDeliverId { get; set; }
        public Guid? SlotTakeId { get; set; }
        public Guid? SlotDeliveryId { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDelete { get; set; }
        public string StoreName { get; set; }
        public string STT { get; set; }
        public string Process { get; set; }
        public string ShipperDeliveryName { get; set; }
        public string ShipperTakeName { get; set; }
    }
}
