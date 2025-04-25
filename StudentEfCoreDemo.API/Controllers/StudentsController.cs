using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Students.Commands;
using StudentEfCoreDemo.Application.Features.Students.Queries;

namespace StudentEfCoreDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var query = new GetStudentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetStudent), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var command = new DeleteStudentCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
} 