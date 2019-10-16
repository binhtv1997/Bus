using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiatDo.Model
{
    public class MyUser : IdentityUser
    {
        public String FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Phone { get; set; }
        public string Problem { get; set; }
    }
}
