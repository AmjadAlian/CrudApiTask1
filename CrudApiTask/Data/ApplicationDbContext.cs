using CrudApiTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApiTask.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base (options) { }

		public DbSet<Employee> employees { get; set; }
		public DbSet<Department> departments { get; set; }
	}
}
