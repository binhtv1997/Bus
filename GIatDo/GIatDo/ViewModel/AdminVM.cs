using System;

namespace GIatDo.ViewModel
{
    public class AdminVM
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid AccountId { get; set; }

    }
    public class AdminCM
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid AccountId { get; set; }
    }
    public class UpdateAdminVM
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid AccountId { get; set; }
    }
    public class LoginVM
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class AdminMD
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

}
