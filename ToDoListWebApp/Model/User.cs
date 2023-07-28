using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApp.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public double AccountBalance { get; set; }
    }
}
