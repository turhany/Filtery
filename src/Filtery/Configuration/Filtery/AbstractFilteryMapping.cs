// ReSharper disable InconsistentNaming
namespace Filtery.Configuration.Filtery
{
    public abstract class AbstractFilteryMapping<TEntity>
    {
        protected readonly FilteryMapper<TEntity> mapper = new FilteryMapper<TEntity>();
    }
}