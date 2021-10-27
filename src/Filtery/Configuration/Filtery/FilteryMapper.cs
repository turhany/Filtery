using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Filtery.Exceptions;

namespace Filtery.Configuration.Filtery
{
    public class FilteryMapper<TEntity>
    {
        private readonly Dictionary<string, Expression<Func<TEntity, object>>> _map = new Dictionary<string, Expression<Func<TEntity, object>>>();
 
        public PropertyMap Name(string name)
        {
            if (!_map.ContainsKey(name))
            {
                _map.Add(name, null);
            }
            else
            {
                throw new MultipleFilterItemConfigurationException($"Multiple filter key detected for \"{name}\"");
            }

            return new PropertyMap(name,this);
        }

        public class PropertyMap
        {
            private readonly FilteryMapper<TEntity> _filteryMapper;
            private readonly string _name;
            
            public PropertyMap(string name,FilteryMapper<TEntity> filteryMapper)
            {
                _name = name.ToLowerInvariant();
                _filteryMapper = filteryMapper;
            }

            public void Property(Expression<Func<TEntity, object>> expression)
            {
                if (!_filteryMapper._map.ContainsKey(_name))
                {
                    throw new NotConfiguredFilterMappingException($"Filter configuration not found for Key: \"{_name}\"");
                }

                _filteryMapper._map[_name] = expression;
            }
        }
    }
}