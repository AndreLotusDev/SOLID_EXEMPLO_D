using EXEMPLO_D_CERTO.Classes;
using System.Collections.Generic;

namespace EXEMPLO_D_CERTO.Interface
{
    public interface IBanco<Elemento> where Elemento : EntidadeBase
    {
        IEnumerable<Elemento> PegaColecao();
        bool Adiciona(Elemento elemento);
        bool Remove(string id);
        bool Atualiza(Elemento elementoAtualizado);
        void LimpaColecao();
        void Dispose();
        IEnumerable<Elemento> PegaElementos();
    }
}
