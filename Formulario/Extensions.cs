using Formulario.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Formulario
{
    public static class Extensions
    {
        static SHA256Managed sha = new SHA256Managed();
        public static string Checksum(this byte[] bytes)
        {
            var checksum = Convert.ToBase64String(sha.ComputeHash(bytes));
            return checksum;
        }

        public static void Add(this ICollection<OpcaoDTO> lst, long ID, string Descricao)
        {
            lst.Add(new OpcaoDTO { OpcaoID = ID, Descricao = Descricao });
        }

        public static void Add(this ICollection<LinhasGradeDTO> lst, long ID, string Descricao)
        {
            lst.Add(new LinhasGradeDTO { LinhaID = ID, Descricao = Descricao });
        }

        public static bool ContainsKey(this ICollection<OpcaoDTO> lst, long ID)
        {
            return lst.Any(d => d.OpcaoID == ID);
        }

        public static OpcaoDTO Find(this ICollection<OpcaoDTO> lst, long ID)
        {
            return lst.SingleOrDefault(d => d.OpcaoID == ID);
        }

        public static bool ContainsKey(this ICollection<LinhasGradeDTO> lst, long ID)
        {
            return lst.Any(d => d.LinhaID == ID);
        }

        public static LinhasGradeDTO Find(this ICollection<LinhasGradeDTO> lst, long ID)
        {
            return lst.SingleOrDefault(d => d.LinhaID == ID);
        }


        public static string GetEnumDescription(this Enum e)
        {
            var get = e.GetType().CustomAttributes.OfType<DescriptionAttribute>().FirstOrDefault();
            if (get != null)
                return get.Description;

            return e.ToString();
        }

        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class => Enum.GetValues(typeof(TEnum))
                                   .Cast<object>()
                                   .Select(value => converter((TEnum)value))
                                   .ToList()
                                   .ForEach(instance => dbSet.AddOrUpdate(instance));

        public static void AtribuirUsuarioID(this List<PerguntaDTO> lst, string UsuarioID)
        {
            lst.ForEach(c => c.UsuarioID = UsuarioID);
        }
    }
}
