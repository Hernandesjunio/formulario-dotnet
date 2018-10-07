using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;

namespace Formulario.Business.Class
{
    public class Filter<T> where T : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortBy { get; set; }
        public bool Descending { get; set; }

        public List<ColumnFilter> ColumnsFilter { get; set; } = new List<ColumnFilter>();

        public IQueryable<T> ApplyFilter(IQueryable<T> query)
        {
            var predicate = PredicateBuilder.True<ModeloDeFormulario>();

            var strb = new StringBuilder();

            var stringOperators = new[] { "Contains", "StartsWith", "EndsWith", "Length" };

            int index = 0;
            foreach (var item in ColumnsFilter)
            {
                var type = Type.GetType(item.PropertyType);

                if (type == typeof(string))
                    item.Value = item.Value.ToString();
                else if (type == typeof(int))
                    item.Value = Convert.ToInt32(item.Value);
                else if (type == typeof(short))
                    item.Value = Convert.ToInt16(item.Value);
                else if (type == typeof(byte))
                    item.Value = Convert.ToByte(item.Value);
                else if (type == typeof(decimal))
                    item.Value = Convert.ToDecimal(item.Value);
                else if (type == typeof(float))
                    item.Value = Convert.ToDouble(item.Value);
                else if (type == typeof(DateTime))
                    item.Value = Convert.ToDateTime(item.Value);
                else
                    throw new NotImplementedException();

                var dot = stringOperators.Contains(item.Operator) ? "." : " ";

                strb.Append($"{item.PropertyName}{dot}{item.Operator} (@{index++})");
            }

            try
            {                
                if (strb.Length > 0)
                    query = query.Where(strb.ToString(), ColumnsFilter.Select(d => d.Value).ToArray());

                if (string.IsNullOrEmpty(SortBy) == false)
                    query = query.OrderBy(SortBy, Descending ? "desc" : "asc");

                query = query.Skip(PageIndex * PageSize).Take(PageSize);


                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
