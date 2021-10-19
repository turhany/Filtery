namespace Filtery.Configuration
{
    public interface IFilteryMapping<TEntity>
    {
        void FilterItemEntityFieldMappings(FilteryMapper<TEntity> filteryMapper);
        void OrderItemEntityFieldMappings(FilteryMapper<TEntity> filteryMapper);
    }
}