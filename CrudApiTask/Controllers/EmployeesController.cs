using CrudApiTask.Data;
using CrudApiTask.Dots.Employee;
using CrudApiTask.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public EmployeesController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet("GetAll")]

		public IActionResult GetAll()
		{
			var employees = context.employees.Select(
				EmpDto => EmpDto.Adapt<GetEmployeesDto>()
				) ;

			return Ok(employees);
		}

		[HttpGet("Details")]
		public IActionResult GetByid(int id) 
		{
			var employee = context.employees.Find(id);
			if(employee == null)
			{
				return NotFound();
			}

			
		  var employeDto = employee.Adapt<DetailsEmployeeDto>();


		  return Ok(employeDto);
		}

		[HttpPost("Create")]
		public IActionResult Create(FormEmployeeDto model)
		{
			var employee = model.Adapt<Employee>();
			context.employees.Add(employee);
			context.SaveChanges();
			return Ok(model);
		}


		[HttpPost("Update")]
		public IActionResult Edit(int id, FormEmployeeDto model)
		{
		  var currentEmp = context.employees.Find(id);

			if(currentEmp == null)
			{
				return NotFound();
			}
			model.Adapt(currentEmp);
			context.SaveChanges();
			return Ok(model);
		}

		[HttpDelete("Remove")]

		public IActionResult Remove(int id)
		{
			var emp = context.employees.Find(id);
			if(emp == null)
			{
				return NotFound();
			}
			context.employees.Remove(emp);
			context.SaveChanges();


			return Ok();
		}

	}
}
