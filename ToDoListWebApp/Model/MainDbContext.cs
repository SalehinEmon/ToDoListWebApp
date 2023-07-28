using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApp.Model
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public MainDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<ToDoName> ToDoNames { get; set; }
        public DbSet<ToDoItem> toDoItems { get; set; }

    }
}
