
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Production_Company.Entities;

#region Database context
namespace Production_Company.DbContexts
{
	public class SqliteDbContext : DbContext
	{
		protected readonly IConfiguration Configuration;
		public SqliteDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}
	
		public DbSet<ProductionType> ProductionTypes { get; set; } 
		public DbSet<Production> Productions { get; set; } 
	
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(Configuration.GetConnectionString("connectionString"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		
			modelBuilder.ApplyConfiguration(new ProductionTypeConfiguration());
			modelBuilder.ApplyConfiguration(new ProductionConfiguration());
	
		}

	}

}



#endregion
