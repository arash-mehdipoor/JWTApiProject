using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Models.Entity
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsRemoved { get; set; }

        public List<Category> Categories { get; set; }

       
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ToDo> ToDos { get; set; }

    }
}
