using System;


namespace GIatDo.ViewModel
{
    public class ServiceUM
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid StoreId { get; set; }
    }

    public class ServiceCM
    {
        public string Description { get; set; }
        public string Price { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid StoreId { get; set; }
        public string Imgurl { get; set; }
    }
    public class ServiceVM
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid StoreId { get; set; }
        public string Imgurl { get; set; }
    }
}
