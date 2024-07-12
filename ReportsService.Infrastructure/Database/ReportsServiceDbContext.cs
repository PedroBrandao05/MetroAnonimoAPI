using Microsoft.EntityFrameworkCore;
using ReportsService.Infrastructure.Models;

namespace ReportsService.Infrastructure.Database;

public class ReportsServiceDbContext : DbContext
{
  public DbSet<Report> Reports { get; set; }
}