using EXEMPLO_D_CERTO.Classes;
using System.Collections.Generic;

namespace EXEMPLO_D_CERTO.Interface
{
    public interface ICrud<Elemento> where Elemento : EntidadeBase
    {
        public void Adiciona(Elemento elemento);
        public void Deleta(string id);
        public void Atualiza(Elemento elemento);
        public List<Elemento> TrazElementos();
    }
}
