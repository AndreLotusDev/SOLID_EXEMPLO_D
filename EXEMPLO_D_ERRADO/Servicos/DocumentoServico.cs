using EXEMPLO_D_ERRADO.Banco;
using EXEMPLO_D_ERRADO.Classes;
using EXEMPLO_D_ERRADO.Interface;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace EXEMPLO_D_ERRADO.Servicos
{
    public class DocumentoServico : ICrud<Documento>
    {
        private GerenciadorBanco<Documento> banco;
        public DocumentoServico()
        {
            banco = new GerenciadorBanco<Documento>();
        }
        public void Adiciona(Documento documento)
        {
            banco.Adiciona(documento);
        }

        public void Atualiza(Documento documento)
        {
            banco.Atualiza(documento);
        }

        public void Deleta(string id)
        {
            banco.Remove(id);
        }

        public List<Documento> TrazElementos()
        {
            return banco.PegaElementos().ToList();
        }

        public void Dispose()
        {
            banco.Dispose();
        }
    }
}
