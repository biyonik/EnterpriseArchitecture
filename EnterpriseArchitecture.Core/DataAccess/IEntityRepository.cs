using System.Linq.Expressions;

namespace EnterpriseArchitecture.Core.DataAccess;

public interface IEntityRepository<TEntity, in TKey>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null);
    TEntity? GetById(TKey id);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
}