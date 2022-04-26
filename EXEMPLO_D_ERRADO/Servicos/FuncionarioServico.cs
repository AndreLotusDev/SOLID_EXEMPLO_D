using EXEMPLO_D_ERRADO.Banco;
using EXEMPLO_D_ERRADO.Classes;
using EXEMPLO_D_ERRADO.Interface;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace EXEMPLO_D_ERRADO.Servicos
{
    public class FuncionarioServico : ICrud<Funcionario>
    {
        private GerenciadorBanco<Funcionario> banco;
        public FuncionarioServico()
        {
            banco = new GerenciadorBanco<Funcionario>();
        }
        public void Adiciona(Funcionario funcionario)
        {
            banco.Adiciona(funcionario);
        }

        public void Atualiza(Funcionario funcionario)
        {
            banco.Atualiza(funcionario);
        }

        public List<Funcionario> TrazElementos()
        {
            return banco.PegaElementos().ToList();
        }

        public void Dispose()
        {
            banco.Dispose();
        }

        public void Deleta(string id)
        {
            banco.Remove(id);
        }
    }
}
