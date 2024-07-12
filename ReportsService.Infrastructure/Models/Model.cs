namespace ReportsService.Infrastructure.Models;

public abstract class Model<TEntity>
{
  public Guid Id { get; set; } = Guid.NewGuid();

  public abstract TEntity ToEntity();

  public abstract Model<TEntity> FromEntity(TEntity entity);
}