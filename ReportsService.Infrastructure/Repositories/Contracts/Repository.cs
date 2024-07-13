using Common.Core;

namespace ReportsService.Infrastructure.Repositories.Contracts;

public interface IRepository<TEntity> where TEntity : Entity
{
  public Task<TEntity?> FindOne(Guid id);

  public Task<List<TEntity>> FindAll();

  public Task Save(TEntity entity);

  public Task Delete(TEntity entity);

  public Task Update(TEntity entity);
}