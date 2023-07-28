using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApp.Model
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int ToDoNameId { get; set; }
        public string ToDoItemName { get; set; }
        public double Amount { get; set; }


    }
}
