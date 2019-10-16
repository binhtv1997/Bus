using System;


namespace GIatDo.ViewModel
{
    public class CustomerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Rate { get; set; }
        public string ImgURL { get; set; }
        public DateTime DateCreate { get; set; }
    }

    public class CustomerCM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Rate { get; set; }
        public Guid AccountId { get; set; }
    }
}
