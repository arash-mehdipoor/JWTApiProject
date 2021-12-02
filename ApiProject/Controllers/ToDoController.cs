using ApiProject.Models.Entity;
using ApiProject.Models.Repasitories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProject.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {

        private readonly ToDoRepasitory _repasitory;

        public ToDoController(ToDoRepasitory repasitory)
        {
            _repasitory = repasitory;
        }


        // GET: api/<TodoController>
        [HttpGet]
        public virtual IActionResult GetAll()
        {
           var todos = _repasitory.GetAll().Select(c => new TodoDto()
            {
                InsertDate = c.InsertDate,
                Text = c.Text,
                Links = new List<Links>()
                {
                    new Links()
                    {
                        Href = Url.Action(nameof(Get),"Todo",new { c.Id},Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    },
                    new Links()
                    {
                        Href = Url.Action(nameof(GetAll),"Todo",Request.Scheme),
                        Rel = "GetAll",
                        Method = "GetAll"
                    }
                }
            });


            return Ok(todos);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var todo = _repasitory.Get(id);
            return Ok(todo);
        }

        public class TodoDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public DateTime InsertDate { get; set; }
            public List<Links> Links { get; set; }
        }
    }
}
