namespace Filtery.Configuration.Filtery
{
    public interface IFilteryMapping<TEntity>
    {
        void FilteryMappings(FilteryMapper<TEntity> mapper);
        //void FilterItemEntityFieldMappings(FilteryMapper<TEntity> mapper);
        //void OrderItemEntityFieldMappings(FilteryMapper<TEntity> filteryMapper);
    }
}