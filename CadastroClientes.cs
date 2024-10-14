using System;
using System.Collections.Generic;

namespace CadastroClientes
{
    class Program
    {
        static List<Cliente> clientes = new List<Cliente>();
        static List<Usuario> usuarios = new List<Usuario>();
        static Usuario usuarioLogado = null;

        static void Main(string[] args)
        {
            bool executando = true;

            // Adiciona um usuário administrador padrão
            usuarios.Add(new Usuario("admin", "admin", true)); // Usuário admin com senha admin

            // Fluxo de login
            Login();

            while (executando)
            {
                Console.WriteLine("\nSelecione uma opção:");
                Console.WriteLine("1 - Adicionar cliente (Admin)");
                Console.WriteLine("2 - Visualizar clientes");
                Console.WriteLine("3 - Editar cliente (Admin)");
                Console.WriteLine("4 - Excluir cliente (Admin)");
                Console.WriteLine("5 - Sair");

                int opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        if (usuarioLogado.IsAdmin)
                            AdicionarCliente();
                        else
                            Console.WriteLine("Permissão negada. Apenas administradores podem adicionar clientes.");
                        break;
                    case 2:
                        VisualizarClientes();
                        break;
                    case 3:
                        if (usuarioLogado.IsAdmin)
                            EditarCliente();
                        else
                            Console.WriteLine("Permissão negada. Apenas administradores podem editar clientes.");
                        break;
                    case 4:
                        if (usuarioLogado.IsAdmin)
                            ExcluirCliente();
                        else
                            Console.WriteLine("Permissão negada. Apenas administradores podem excluir clientes.");
                        break;
                    case 5:
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        static void Login()
        {
            bool loginBemSucedido = false;
            while (!loginBemSucedido)
            {
                Console.WriteLine("Digite seu nome de usuário:");
                string nomeUsuario = Console.ReadLine();

                Console.WriteLine("Digite sua senha:");
                string senha = Console.ReadLine();

                // Verifica se o usuário existe e se a senha está correta
                usuarioLogado = usuarios.Find(u => u.NomeUsuario == nomeUsuario && u.Senha == senha);

                if (usuarioLogado != null)
                {
                    Console.WriteLine($"Bem-vindo, {usuarioLogado.NomeUsuario}!");
                    loginBemSucedido = true;
                }
                else
                {
                    Console.WriteLine("Nome de usuário ou senha incorretos. Tente novamente.");
                }
            }
        }

        static void AdicionarCliente()
        {
            Console.WriteLine("Digite o nome do cliente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o e-mail do cliente: ");
            string email = Console.ReadLine();

            Cliente cliente = new Cliente(nome, email);
            clientes.Add(cliente);

            Console.WriteLine("Cliente adicionado com sucesso.");
        }

        static void VisualizarClientes()
        {
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine("Nome: " + cliente.Nome);
                Console.WriteLine("E-mail: " + cliente.Email);
                Console.WriteLine("----------------------");
            }
        }

        static void EditarCliente()
        {
            Console.WriteLine("Digite o nome do cliente que deseja editar: ");
            string nome = Console.ReadLine();

            Cliente cliente = clientes.Find(c => c.Nome == nome);

            if (cliente != null)
            {
                Console.WriteLine("Digite o novo nome do cliente: ");
                string novoNome = Console.ReadLine();

                Console.WriteLine("Digite o novo e-mail do cliente: ");
                string novoEmail = Console.ReadLine();

                cliente.Nome = novoNome;
                cliente.Email = novoEmail;

                Console.WriteLine("Cliente editado com sucesso.");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
        }

        static void ExcluirCliente()
        {
            Console.WriteLine("Digite o nome do cliente que deseja excluir: ");
            string nome = Console.ReadLine();

            Cliente cliente = clientes.Find(c => c.Nome == nome);

            if (cliente != null)
            {
                clientes.Remove(cliente);
                Console.WriteLine("Cliente excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
        }
    }

    // Classe Cliente
    class Cliente
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }

    // Classe Usuario para controle de acesso
    class Usuario
    {
        public string NomeUsuario { get; private set; }
        public string Senha { get; private set; }
        public bool IsAdmin { get; private set; }

        public Usuario(string nomeUsuario, string senha, bool isAdmin)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
            IsAdmin = isAdmin;
        }
    }
}
