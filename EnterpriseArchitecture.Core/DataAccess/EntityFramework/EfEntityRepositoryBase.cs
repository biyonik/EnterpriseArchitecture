using System.Linq.Expressions;
using EnterpriseArchitecture.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseArchitecture.Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TKey, TContext>: IEntityRepository<TEntity, TKey> 
    where TContext : DbContext, new() 
    where TEntity : class, IEntityWithId<TKey>, IEntity, new()
{
    public bool Add(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry<TEntity>(entity);
        addedEntity.State = EntityState.Added;
        return context.SaveChanges() > 0;
    }

    public bool Update(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry<TEntity>(entity);
        addedEntity.State = EntityState.Modified;
        return context.SaveChanges() > 0;
    }

    public bool Delete(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry<TEntity>(entity);
        addedEntity.State = EntityState.Deleted;
        return context.SaveChanges() > 0;
    }

    public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter)
    {
        using var context = new TContext();
        return filter == null
            ? context.Set<TEntity>().ToList()
            : context.Set<TEntity>().Where(filter).ToList();
    }

    public TEntity? GetById(TKey id)
    {
        using var context = new TContext();
        return context.Set<TEntity>().FirstOrDefault(x => x.Equals(id));
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using var context = new TContext();
        return context.Set<TEntity>().SingleOrDefault(filter);
    }
}