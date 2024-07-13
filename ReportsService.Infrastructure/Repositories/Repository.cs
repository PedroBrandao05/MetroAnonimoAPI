using Microsoft.EntityFrameworkCore;
using Common.Core;
using ReportsService.Infrastructure.Database;
using ReportsService.Infrastructure.Models;
using ReportsService.Infrastructure.Repositories.Contracts;

namespace ReportsService.Infrastructure.Repositories;

public abstract class Repository<TEntity, TDatabaseModel> : IRepository<TEntity> where TEntity : Entity where TDatabaseModel : Model<TEntity>, new ()
{
  protected readonly ReportsServiceDbContext _dbContext;

  protected DbSet<TDatabaseModel> _dbSet;

  public Repository(ReportsServiceDbContext dbContext, DbSet<TDatabaseModel> dbSet)
  {
    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    _dbSet = dbSet;
  }

  public virtual Task<TEntity?> FindOne(Guid id)
  {
    var e = _dbSet.Where(e => e.Id == id).ToList().FirstOrDefault();
    return Task.FromResult(e?.ToEntity());
  }

  public async Task<List<TEntity>> FindAll()
  {
    var e = await _dbSet.Where(e => true).ToListAsync();
    return e.Select(e => e.ToEntity()).ToList();
  }

  public async Task Save(TEntity entity)
  {
    _dbSet.Add(FromDomainModel(entity));
    await _dbContext.SaveChangesAsync();
  }

  public async Task Delete(TEntity entity)
  {
    _dbSet.Remove(FromDomainModel(entity));
    await _dbContext.SaveChangesAsync();
  }

  public async Task Update(TEntity entity)
  {
    _dbSet.Update(FromDomainModel(entity));
    await _dbContext.SaveChangesAsync();
  }

  public abstract TDatabaseModel FromDomainModel(TEntity e);
}