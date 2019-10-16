using System;
using System.Collections.Generic;

namespace GIatDo.ViewModel
{
    public class CreateServiceTypeVM
    {
        public string Name { get; set; }
    }
    public class UpdateServiceTypeVM
    {
        public Guid Id { get; set; } 
        public bool IsDelete { get; set; }
    }
    public class ServiceTypeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public List<ServiceVM> listService { get; set; }
    }
}
