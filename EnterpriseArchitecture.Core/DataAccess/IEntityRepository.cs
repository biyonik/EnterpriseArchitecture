using System.Linq.Expressions;

namespace EnterpriseArchitecture.Core.DataAccess;

public interface IEntityRepository<TEntity, in TKey>
{
    bool Add(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null);
    TEntity? GetById(TKey id);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
}