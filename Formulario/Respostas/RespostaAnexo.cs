using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.DTO;
using Formulario.Class;

namespace Formulario.Respostas
{
    

    public class RespostaAnexo : Resposta
    {
        public virtual RespostaAnexoContent Valor { get; set; }
        public long? AnexoID { get; set; }

        public override void AtribuirResposta(RespostaDTO resposta)
        {
            Valor = new Respostas.RespostaAnexoContent
            {
                AnexoID = 0,
                Checksum = null,
                Conteudo = Convert.FromBase64String((resposta.Valor ?? "").ToString()),
                Extensao = resposta.Extensao,
                Nome = resposta.NomeArquivo,
                //Resposta = null,
                //RespostaID = resposta.RespostaID,
                UniqueIdentifier = Guid.NewGuid(),
                ControleUsuario = new ComplexProperties.ControleUsuario
                {
                    Data = DateTime.Now,
                    UsuarioID = resposta.UsuarioID
                }
            }.ComputeChecksum();

            Valor.Checksum = Valor.Conteudo.Checksum();
        }

        public override bool Validar()
        {
            var pAnexo = Pergunta as PerguntaAnexo;

            if (Pergunta.Obrigatorio && (Valor == null || Valor.Conteudo.Length == 0))
                return false;

            if (Valor != null && pAnexo.TamanhoMaximoBytes < Valor.Conteudo.Length)
                return false;

            return true;
        }
    }
}
