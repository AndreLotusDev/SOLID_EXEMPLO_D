using EXEMPLO_D_CERTO.Classes;
using EXEMPLO_D_CERTO.Interface;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace EXEMPLO_D_CERTO.Servicos
{
    public class DocumentoServico : ICrud<Documento>
    {
        private IBanco<Documento> _banco;
        public DocumentoServico(IBanco<Documento> banco)
        {
            _banco = banco;
        }
        public void Adiciona(Documento documento)
        {
            _banco.Adiciona(documento);
        }

        public void Atualiza(Documento documento)
        {
            _banco.Atualiza(documento);
        }

        public void Deleta(string id)
        {
            _banco.Remove(id);
        }

        public List<Documento> TrazElementos()
        {
            return _banco.PegaElementos().ToList();
        }

        public void Dispose()
        {
            _banco.Dispose();
        }
    }
}
