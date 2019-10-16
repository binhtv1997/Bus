using System;


namespace GIatDo.ViewModel
{
    public class ShipperVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Rate { get; set; }
    }

    public class CreateShipperVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Rate { get; set; }
        public Guid? AccountId { get; set; }
    }
}
