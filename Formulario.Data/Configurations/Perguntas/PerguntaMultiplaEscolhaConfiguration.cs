﻿using Formulario.Business.Class;
using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Configurations.Perguntas
{
    

    

    

    public class PerguntaMultiplaEscolhaConfiguration : EntityTypeConfiguration<PerguntaMultiplaEscolha>
    {
        public PerguntaMultiplaEscolhaConfiguration()
        {
            Property(c => c.TipoEntradaID).HasColumnName("TipoEntradaID");
                        
        }
    }
}
