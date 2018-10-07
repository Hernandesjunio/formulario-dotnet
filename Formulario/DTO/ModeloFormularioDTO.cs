using Formulario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.DTO
{
    public class ModeloFormularioDTO
    {
        /// <summary>
        /// Inicializa as condições padrões
        /// </summary>
        public ModeloFormularioDTO()
        {
            LstOperacaoCondicional = new Dictionary<eTipoPergunta, Dictionary<short, string>>();
            
            var type = typeof(eOperacaoCondicional);

            foreach (var item in Enum.GetValues(type).Cast<eOperacaoCondicional>())
            {
                if ((byte)item <= 0)
                    continue;

                var field = type.GetField(item.ToString());

                var tipoPergunta = field.CustomAttributes
                    .Where(c => c.AttributeType == typeof(TipoPerguntaAttribute))
                    .SelectMany(e => e.ConstructorArguments.Select(c => (eTipoPergunta)c.Value))
                    .ToList();

                if (tipoPergunta.Any() == false)
                    throw new ApplicationException($"O item {item.ToString()} da operação condicional, não está vinculado com um tipo de pergunta. TipoPerguntaAttribute");

                var tp = tipoPergunta.Single();

                if (LstOperacaoCondicional.ContainsKey(tp) == false)
                    LstOperacaoCondicional.Add(tp, new Dictionary<short, string>());

                LstOperacaoCondicional[tp].Add((short)item, item.GetEnumDescription());
            }
       }

        /// <summary>
        /// Lista de perguntas que compõe o formulário
        /// </summary>
        public List<PerguntaDTO> Perguntas { get; set; }

        /// <summary>
        /// Lista de perguntas operações para as condicionais conforme o tipo de cada pergunta
        /// </summary>
        public Dictionary<eTipoPergunta, Dictionary<short, string>> LstOperacaoCondicional { get; set; }

        /// <summary>
        /// Código do modelo de formulário
        /// </summary>
        public long ModeloFormularioID { get; set; }
        public string Descricao { get; set; }
        public string Html { get; set; }
    }
}
