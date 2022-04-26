using EXEMPLO_D_ERRADO.Banco;
using EXEMPLO_D_ERRADO.Classes;
using EXEMPLO_D_ERRADO.Servicos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EXEMPLO_D_ERRADO
{
    class Program
    {
        static void Main(string[] args)
        {
            //-----------------------------------------LIMPEZA DE BANCO (FUNCIONARIOS)

            GerenciadorBanco<Funcionario> gerenciadorBanco = new();
            gerenciadorBanco.LimpaColecao();
            gerenciadorBanco.Dispose();

            GerenciadorBanco<Documento> gerenciadorDocumento = new();
            gerenciadorDocumento.LimpaColecao();
            gerenciadorDocumento.Dispose();

            //---------------------------------------FUNCIONARIO AREA

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            var funcionarioUm = new Funcionario("Andre Soares Gomes", 1_500);
            var funcioarioDois = new Funcionario("Barbara Silveira Souza", 2_700);

            FuncionarioServico funcionarioServico = new();
            funcionarioServico.Adiciona(funcionarioUm);
            funcionarioServico.Adiciona(funcioarioDois);

            foreach (var funcionario in funcionarioServico.TrazElementos())
            {
                Console.WriteLine(funcionario.ToString());
            }

            funcionarioServico.Dispose();

            Console.WriteLine("-------------------------------------------------------");

            //-------------------------------------DOCUMENTO AREA

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            var documentoUm = new Documento("DOCUMENTO_IDENTIFICACAO", "Serve para identificar o funcionario");
            var documentoDois = new Documento("DOCUMENTO_SALARIO", "Serve para identificar o funcionario");

            DocumentoServico documentoServico = new();
            documentoServico.Adiciona(documentoUm);
            documentoServico.Adiciona(documentoDois);

            foreach (var documento in documentoServico.TrazElementos())
            {
                Console.WriteLine(documento.ToString());
            }

            documentoServico.Dispose();

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
