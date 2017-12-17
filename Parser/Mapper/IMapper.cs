using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Model;
using System.Reflection;

namespace Parser.Mapper
{
    public interface IMapper<T> where T : IMapperEntity
    {
        T Map(DataRow row);

        IEnumerable<T> Map(DataTable table);

        T SetProperty(T entity, PropertyInfo propertyInfo, string value);
    }
}
