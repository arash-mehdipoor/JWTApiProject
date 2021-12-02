using ApiProject.Models.Context;
using ApiProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Models.Repasitories
{
    public class ToDoRepasitory
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoRepasitory(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ToDo> GetAll()
        {
            return _dbContext.ToDos.ToList();
        }

        public ToDo Get(int id)
        {
            return _dbContext.ToDos.FirstOrDefault(t => t.Id == id);
        }
    }
}
