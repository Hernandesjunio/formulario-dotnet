﻿using Formulario;
using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Class
{
    public class TipoOperacaoCondicional
    {        
        public TipoOperacaoCondicional()
        {

        }

        public byte TipoOperacaoCondicionalID { get; set; }
        public string Descricao { get; set; }

        public byte TipoPerguntaID { get; set; }
        public virtual TipoPergunta TipoPergunta { get; set; }
    }
}
