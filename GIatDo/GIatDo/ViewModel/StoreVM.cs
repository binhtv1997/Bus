using System;
using System.Collections.Generic;

namespace GIatDo.ViewModel
{
    public class StoreVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float Rate { get; set; }
        public Guid? AccountId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public string Imgurl { get; set; }
        public bool IsActive { get; set; }
        public List<ServiceTypeVM> ServiceTypes { get; set; }
    }
    public class StoreUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float Rate { get; set; }
        public Guid? AccountId { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public string Imgurl { get; set; }
        public bool IsActive { get; set; }
    }
    public class StoreCM
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float Rate { get; set; }
        public Guid? AccountId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public string Imgurl { get; set; }

    }
    public class StoreHasUse
    {
        public Guid CustomerId { get; set; }
        public Guid ServiceTypeId { get; set; }

    }
}
