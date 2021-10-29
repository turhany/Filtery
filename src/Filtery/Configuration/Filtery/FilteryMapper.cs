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
 
        public FilterMap Name(string name)
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

            public FilterOperationMap Filter(Expression<Func<TEntity, bool>> expression)
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                if (expression == null)
                {
                    throw new NotConfiguredFilterMappingException($"Filter Expression not found for Key: \"{_name}\"");
                }

                _filteryMapper._map[_name] = new FilteryMappingItem<TEntity>
                {
                    Expression = expression
                };

                return new FilterOperationMap(_name, _filteryMapper);
            } 
        }
        
        public class FilterOperationMap
        {
            private readonly FilteryMapper<TEntity> _filteryMapper;
            private readonly string _name;
            
            public FilterOperationMap(string name,FilteryMapper<TEntity> filteryMapper)
            {
                _name = name.ToLowerInvariant();
                _filteryMapper = filteryMapper;
            }

            public OrderMap FilterOperations(params FilterOperation[] filterOperations)
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                if (filterOperations == null || !filterOperations.Any())
                {
                    throw new NotConfiguredFilterMappingException($"Filter Operation not found for Key: \"{_name}\"");
                }

                _filteryMapper._map[_name].FilterOperations = filterOperations.ToList();

                return new OrderMap(_name, _filteryMapper);
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

            public void OrderProperty(Expression<Func<TEntity, object>> expression)
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                if (expression == null)
                {
                    throw new NotConfiguredFilterMappingException($"Order Expression not found for Key: \"{_name}\"");
                }

                _filteryMapper._map[_name].OrderExpression = expression;
            } 
        }
    }
}