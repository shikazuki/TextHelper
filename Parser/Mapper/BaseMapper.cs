﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Model;
using System.Reflection;

namespace Parser.Mapper
{
    public class BaseMapper<T> : IMapper<T> where T : IMapperEntity
    {
        public T Map(DataRow row)
        {
            T entity = Activator.CreateInstance<T>();
            var properties = entity.GetType().GetProperties();
            var headerNames = row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName);

            var propertyNames = properties.Select(p => p.Name).ToList();
            foreach(var headerName in headerNames)
            {
                if (!string.IsNullOrWhiteSpace(headerName) && propertyNames.Contains(headerName))
                {
                    var value = row[headerName] as string;
                    if (!string.IsNullOrEmpty(value))
                    {
                        var prop = properties.Where(p => p.Name == headerName).First();
                        entity = SetProperty(entity, prop, value);
                        
                    }
                }
            }
            return entity;
        }

        public IEnumerable<T> Map(DataTable table)
        {
            List<T> list = new List<T>();

            foreach(DataRow row in table.Rows)
            {
                var entity = Map(row);
                list.Add(entity);
            }

            return list;
        }

        public T SetProperty(T entity, PropertyInfo propertyInfo, string value)
        {
            var valueType = propertyInfo.PropertyType;
            var parseValue = Convert.ChangeType(value, valueType);
            propertyInfo.SetValue(entity, parseValue);
            return entity;
        }
    }
}
