using System;


namespace GIatDo.ViewModel
{
    public class OrderServiceVM
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ServiceId { get; set; }
    }
    public class OrderServiceUM
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ServiceId { get; set; }
    }
    public class OrderServiceCM
    {
        public string Quantity { get; set; }
        public string Price { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ServiceId { get; set; }
    }

}
