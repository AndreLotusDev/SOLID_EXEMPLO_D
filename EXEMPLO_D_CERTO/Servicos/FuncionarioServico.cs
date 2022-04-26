using EXEMPLO_D_CERTO.Bancos;
using EXEMPLO_D_CERTO.Classes;
using EXEMPLO_D_CERTO.Interface;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace EXEMPLO_D_CERTO.Servicos
{
    public class FuncionarioServico : ICrud<Funcionario>
    {
        private IBanco<Funcionario> _banco;
        public FuncionarioServico(IBanco<Funcionario> banco)
        {
            _banco = banco;
        }
        public void Adiciona(Funcionario funcionario)
        {
            _banco.Adiciona(funcionario);
        }

        public void Atualiza(Funcionario funcionario)
        {
            _banco.Atualiza(funcionario);
        }

        public List<Funcionario> TrazElementos()
        {
            return _banco.PegaElementos().ToList();
        }

        public void Dispose()
        {
            _banco.Dispose();
        }

        public void Deleta(string id)
        {
            _banco.Remove(id);
        }
    }
}
