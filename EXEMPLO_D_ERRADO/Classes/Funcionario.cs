namespace EXEMPLO_D_ERRADO.Classes
{
    public class Funcionario : EntidadeBase
    {
        public string Nome { get; set; }
        public double Salario { get; set; }
        public Funcionario() { }
        public Funcionario(string nome, double salario)
        {
            Nome = nome;
            Salario = salario;
        }

        public override string ToString()
        {
            return $"Nome: {Nome} | Salário: {Salario} | Id: {Id}";
        }
    }
}
