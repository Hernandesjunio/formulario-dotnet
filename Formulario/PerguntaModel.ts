export class Pergunta{
     Deleted: boolean;
     PerguntaID:number;
     Titulo:string;
     Descricao:string;
     TipoPergunta:eTipoPergunta;
     Opcoes: Dicionario[];
     LinhasGrade: Dicionario[];
     PerguntaCondicional: any;
     PerguntaCondicionalID: any;
     TipoEntrada: any;
     PatternRegex: any;
     TamanhoMaximo: any;
     TamanhoMaximoBytes: any;
     Validador: any;
     LeiautePergunta: any;
     Prefixo: any;
     CasasDecimais: any;
     Sufixo: any;
     UsuarioID: any;
}

export class Resposta {
    RespostaID: any;
    PerguntaID: any;
    Opcoes: any;
    OpcaoID: any;
    RespostaGrade: any;
    Valor: any;
    UsuarioID: any;
}

export enum eTipoPergunta{
    Texto=1,
    Anexo=2,
    EscolhaUnica=3,
    MultiplaEscolha=4,
    Numero=5,
    Data=6,
}