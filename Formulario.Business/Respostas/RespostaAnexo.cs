using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.Business.DTO;
using Formulario.Business.Class;

namespace Formulario.Business.Respostas
{    
    public class RespostaAnexo : Resposta
    {
        public virtual RespostaAnexoContent Valor { get; set; }
        public long? AnexoID { get; set; }

        public override Resposta AtribuirResposta(RespostaDTO resposta)
        {
            if (Valor == null)
                Valor = new RespostaAnexoContent();

            Valor.AnexoID = Valor?.AnexoID ?? 0;
            Valor.Checksum = null;
            Valor.Conteudo = Convert.FromBase64String((resposta.Valor ?? "").ToString());
            Valor.Extensao = resposta.Extensao;
            Valor.Nome = resposta.NomeArquivo;                        
            Valor.ControleUsuario = new ComplexProperties.ControleUsuario
            {
                Data = DateTime.Now,
                UsuarioID = resposta.UsuarioID
            };

            Valor.ComputeChecksum();

            return this;
        }

        public override bool Validar()
        {
            var pAnexo = Pergunta as PerguntaAnexo;

            if (Pergunta.Obrigatorio && (Valor == null || Valor.Conteudo == null || Valor.Conteudo.Length == 0))
                return false;

            if (Valor != null && pAnexo.TamanhoMaximoBytes < Valor.Conteudo?.Length)
                return false;

            return true;
        }
    }
}
