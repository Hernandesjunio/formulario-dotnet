using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Class
{
    public class ColumnFilter
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }
}
