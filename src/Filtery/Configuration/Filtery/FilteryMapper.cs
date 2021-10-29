using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Filtery.Exceptions;
using Filtery.Models.Filter;

namespace Filtery.Configuration.Filtery
{
    public class FilteryMapper<TEntity>
    {
        private readonly Dictionary<string, FilteryMappingItem<TEntity>> _map = new Dictionary<string, FilteryMappingItem<TEntity>>();
 
        public OrderMap Name(string name)
        {
            if (!_map.ContainsKey(name))
            {
                _map.Add(name, null);
            }
            else
            {
                throw new MultipleFilterItemConfigurationException($"Multiple filter key detected for \"{name}\"");
            }

            return new OrderMap(name,this);
        }
        
        public FilterMap NameWithoutOrder(string name)
        {
            if (!_map.ContainsKey(name))
            {
                _map.Add(name, null);
            }
            else
            {
                throw new MultipleFilterItemConfigurationException($"Multiple filter key detected for \"{name}\"");
            }

            return new FilterMap(name,this);
        }
        
        public class FilterMap
        {
            private readonly FilteryMapper<TEntity> _filteryMapper;
            private readonly string _name;
            
            public FilterMap(string name,FilteryMapper<TEntity> filteryMapper)
            {
                _name = name.ToLowerInvariant();
                _filteryMapper = filteryMapper;
            }

            public FilterMap Filter(Expression<Func<TEntity, bool>> expression, params FilterOperation[] filterOperations )
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                if (expression == null)
                {
                    throw new NotConfiguredFilterMappingException($"Filter Expression not found for Key: \"{_name}\"");
                }
                
                if (filterOperations == null || !filterOperations.Any())
                {
                    throw new NotConfiguredFilterMappingException($"Filter Operation not found for Key: \"{_name}\"");
                }

                if (_filteryMapper._map[_name] == null)
                {
                    _filteryMapper._map[_name] = new FilteryMappingItem<TEntity>();
                }

                if (_filteryMapper._map[_name].FilteryMappings == null)
                {
                    _filteryMapper._map[_name].FilteryMappings = new List<FilteryMapping<TEntity>>()
                    {
                        new FilteryMapping<TEntity> {Expression = expression, FilterOperations = filterOperations.ToList()}
                    };
                }
                else
                {
                    var mappings = _filteryMapper._map[_name].FilteryMappings;
                    var selectedFilterOperations = mappings.SelectMany(p => p.FilterOperations).ToList();

                    foreach (var filterOperation in filterOperations)
                    {
                        if (selectedFilterOperations.Contains(filterOperation))
                        {
                            throw new MultipleFilterItemConfigurationException($"Multiple filter operation '{filterOperation.ToString()}' detected for \"{_name}\"");
                        }
                    }
                    
                    _filteryMapper._map[_name].FilteryMappings.Add(new FilteryMapping<TEntity> {Expression = expression, FilterOperations = filterOperations.ToList()});
                }

                return new FilterMap(_name, _filteryMapper);
            } 
        }
        
        public class OrderMap
        {
            private readonly FilteryMapper<TEntity> _filteryMapper;
            private readonly string _name;
            
            public OrderMap(string name,FilteryMapper<TEntity> filteryMapper)
            {
                _name = name.ToLowerInvariant();
                _filteryMapper = filteryMapper;
            }

            public FilterMap OrderProperty(Expression<Func<TEntity, object>> expression)
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                if (expression == null)
                {
                    throw new NotConfiguredFilterMappingException($"Order Expression not found for Key: \"{_name}\"");
                }

                _filteryMapper._map[_name] = new FilteryMappingItem<TEntity>
                {
                    OrderExpression = expression
                };

                return new FilterMap(_name, _filteryMapper);
            } 
        }
    }
}