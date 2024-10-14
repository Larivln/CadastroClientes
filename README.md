Sistema de Cadastro de Clientes
Este é um sistema simples de cadastro de clientes feito em C# com funcionalidades de adicionar, visualizar, editar e excluir clientes. O sistema também conta com um controle de acesso e permissões, permitindo que apenas usuários administradores possam realizar certas operações (como adicionar, editar e excluir clientes).

Funcionalidades
O sistema oferece as seguintes funcionalidades principais: adicionar cliente (somente administradores), visualizar clientes (todos os usuários), editar cliente (somente administradores), excluir cliente (somente administradores), login de usuário com credenciais de nome de usuário e senha, e controle de permissões diferenciando usuários administradores e regulares.

Pré-requisitos
Para rodar o projeto, é necessário ter o .NET SDK 5.0 ou superior instalado, além de um editor de texto ou IDE que suporte C#, como Visual Studio ou Visual Studio Code.

Como Rodar o Projeto
Clone o repositório com o comando:


Navegue até o diretório do projeto:


cd sistema-cadastro-clientes
Compile e execute o projeto usando o comando:


dotnet run
Utilize as credenciais de login padrão para acessar o sistema: Nome de usuário: admin, Senha: admin.

Estrutura do Projeto
O código está organizado da seguinte maneira:

Program.cs: Contém a lógica principal do programa, incluindo o fluxo de controle de clientes e permissões de acesso.
Cliente.cs: Define a classe Cliente, que possui as propriedades Nome e Email.
Usuario.cs: Define a classe Usuario, responsável por gerenciar nome de usuário, senha e o nível de permissão (administrador ou regular).
Funcionalidades Futuras
Aqui estão algumas funcionalidades que podem ser adicionadas no futuro: salvar e carregar clientes de um arquivo para persistir os dados entre execuções, criptografia de senhas para aumentar a segurança e permitir o cadastro de novos usuários diretamente no sistema (apenas por administradores).

Exemplos de Uso
Adicionar um Cliente (Apenas Administradores)
Após o login como administrador, você pode adicionar clientes ao sistema fornecendo nome e e-mail, com a seguinte interação:


Digite o nome do cliente: John Doe  
Digite o e-mail do cliente: john.doe@example.com  
Cliente adicionado com sucesso.
Visualizar Clientes (Todos os Usuários)
Todos os usuários podem visualizar os clientes cadastrados:


Nome: John Doe  
E-mail: john.doe@example.com  
----------------------
Editar um Cliente (Apenas Administradores)
Somente administradores podem editar clientes existentes:


Digite o nome do cliente que deseja editar: John Doe  
Digite o novo nome do cliente: Johnny Doe  
Digite o novo e-mail do cliente: johnny.doe@example.com  
Cliente editado com sucesso.
Excluir um Cliente (Apenas Administradores)
Apenas administradores podem excluir clientes cadastrados:


Digite o nome do cliente que deseja excluir: Johnny Doe  
Cliente excluído com sucesso.
Código Fonte
Aqui está um trecho do código principal usado para implementar o sistema:



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
}
Contribuindo
Contribuições são bem-vindas! Sinta-se à vontade para abrir um pull request ou relatar problemas no repositório.