using ApiProject.Models.Entity;
using ApiProject.Models.Repasitories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ToDoController
    {
        private readonly ToDoRepasitory _repasitory;

        public ValuesController(ToDoRepasitory repasitory) : base(repasitory)
        {
            _repasitory = repasitory;
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            var todos = _repasitory.GetAll().Select(c => new TodoDto()
            {
                InsertDate = c.InsertDate,
                Text = c.Text,
                Links = new List<Links>()
                {
                    new Links()
                    {
                        Href = Url.Action(nameof(Get),"Values",new { c.Id},Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    }
                }
            });


            return Ok(todos);
        }

        [HttpGet("{id}")]
        public override IActionResult Get(int id)
        {
            var todo = _repasitory.Get(id);
            return Ok(todo);
        }
    }
}
