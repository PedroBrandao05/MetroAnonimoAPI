using Microsoft.EntityFrameworkCore;
using ReportsService.Infrastructure.Database;

namespace ReportsService.Infrastructure.Repositories;

public class Report : Repository<Entities.Report, Models.Report>
{
  public Report(ReportsServiceDbContext dbContext, DbSet<Models.Report> dbSet) : base(dbContext, dbSet)
  {
  }

  public override Models.Report FromDomainModel(Entities.Report e)
  {
    var model = new Models.Report();

    return model.FromEntity(e);
  }
}