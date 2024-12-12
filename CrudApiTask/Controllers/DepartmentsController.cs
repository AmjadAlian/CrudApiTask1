using CrudApiTask.Data;
using CrudApiTask.Dots.Department;
using CrudApiTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public DepartmentsController(ApplicationDbContext context )
		{
			this.context = context;
		}

		[HttpGet("GetAll")]

		public IActionResult GetAll()
		{
			var departments = context.departments.Select(
				depDto =>new GetDepartmentDto
				{
					Id = depDto.Id,
					Name = depDto.Name,
				}
				);

			
			return Ok(departments);
		}

		[HttpGet("Details")]
		public IActionResult GetByid(int id)
		{
			var department = context.departments.Find(id);
			if (department == null)
			{
				return NotFound();
			}
			var depDto = new DetailsDepartmentDto()
			{
				Id=id,
				Name = department.Name,
				Description = department.Description,

			};


			return Ok(depDto);
		}

		[HttpPost("Create")]
		public IActionResult Create(CreateDepartmentDto model)
		{
			var dep = new Department()
			{
				Name = model.Name,
				Description = model.Description,
			};
			context.departments.Add(dep);
			context.SaveChanges();
			return Ok(model);
		}


		[HttpPost("Update")]
		public IActionResult Edit(int id, CreateDepartmentDto model)
		{
			var currentDep = context.departments.Find(id);

			if (currentDep == null)
			{
				return NotFound();
			}
			currentDep.Name = model.Name;
			currentDep.Description = model.Description;
			context.SaveChanges();
			return Ok(model);
		}

		[HttpDelete("Remove")]

		public IActionResult Remove(int id)
		{
			var dep = context.departments.Find(id);
			if (dep == null)
			{
				return NotFound();
			}
			context.departments.Remove(dep);
			context.SaveChanges();


			return Ok();
		}
	}
}
