using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Persistencia.Repositories;
using SqlDataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class Menu
    {
        const int opcao = 0;        
        bool loop = true;

        String printLineTop = new String('#', 36);
        String printLineFooter = new String('#', 78);
        String printLineSpace = new String(' ', 76);
        String lineSair = new String(' ', 68);
        String lineAdicionar = new String(' ', 55);
        String lineConsultaNome = new String(' ', 46);
        String lineLista = new String(' ', 45);
        String lineExcluir = new String(' ', 57);
        String lineEditar = new String(' ', 58);
        String lineCarregar = new String(' ', 45);
        String lineGravar = new String(' ', 44);

        private IRepositoryUsuario _repositoryUsuario;
        private IRepositoryFileDbUsuario _repositoryFileDbUsuario;
        private ICalculaDiasAniversario _calculaDias;

        public Menu()
        {
            _repositoryUsuario = new RepositoryUsuario();
            _repositoryFileDbUsuario = new RepositoryFileDbUsuario();
            _calculaDias = new GestorDiasAniversario();
        }

        public Menu(IRepositoryUsuario repositoryUsuario, ICalculaDiasAniversario calculaDias, 
                    IRepositoryFileDbUsuario repositoryFileDbUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryFileDbUsuario = repositoryFileDbUsuario;
            _calculaDias = calculaDias;
        }

        public void PrintMenu()
        {            
            while (loop)
            {
                Console.WriteLine();
                Console.WriteLine(" " + printLineTop + " MENU " + printLineTop);
                Console.WriteLine(" #" + printLineSpace + "#");
                Console.WriteLine(" # " + "0. SAIR" + lineSair + "#");
                Console.WriteLine(" # " + "1. Adicionar Contato" + lineAdicionar + "#");
                Console.WriteLine(" # " + "2. Consultar Contato por Nome" + lineConsultaNome + "#");
                Console.WriteLine(" # " + "3. Consultar lista de Contatos" + lineLista + "#");
                Console.WriteLine(" # " + "4. Excluir Contato" + lineExcluir + "#");
                Console.WriteLine(" # " + "5. Editar Contato" + lineEditar + "#");
                Console.WriteLine(" # " + "7. Grava os dados em um arquivo" + lineGravar + "#");
                Console.WriteLine(" # " + "6. Carrega dados de um arquivo" + lineCarregar + "#");                
                Console.WriteLine(" #" + printLineSpace + "#");
                Console.WriteLine(" " + printLineFooter);
                Console.WriteLine();

                FindByBirthDayPerson();

                Console.Write("\n\n\tDigite uma opção: ");
                try
                {
                    int opcao = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (opcao)
                    {
                        case 1:
                            Console.Write("\t\nEntre com o primeiro nome: ");
                            String _primNome = Console.ReadLine();

                            Console.Write("\n\tEntre com o sobrenome: ");
                            String _sobreNome = Console.ReadLine();

                            Console.Write("\n\tEntre com a data de nascimento (dd/mm/aaaa): ");
                            DateTime _dataNasc = DateTime.ParseExact(Console.ReadLine(),
                                                            "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            var usuario = new Usuario
                            {
                                Nome = _primNome,
                                SobreNome = _sobreNome,
                                Nascimento = _dataNasc
                            };

                            _repositoryUsuario.Save(usuario);
                            Console.WriteLine("\n\tAmigo adicionado com sucesso!");

                            break;

                        case 2:

                            Console.Write("\tConsultar por nome: ");
                            String nome = Console.ReadLine();

                            var usuarios = _repositoryUsuario.GetAll(nome);

                            for (int i = 0; i < usuarios.Count; i++)
                            {
                                var data = usuarios[i].Nascimento;
                                usuarios[i].ProxAniv = _calculaDias.CacularDiasAniversario(data);

                                if (usuarios[i].ProxAniv == 0)
                                {
                                    Console.WriteLine("\n\tAniversariante(s) do dia!");
                                    Console.WriteLine($"\n\t{usuarios[i].Nome} {usuarios[i].SobreNome}");
                                }
                                else
                                {
                                    Console.WriteLine($"\n\t{usuarios[i].Nome} {usuarios[i].SobreNome}");
                                    Console.WriteLine($"\n\tFaltam {usuarios[i].ProxAniv} "
                                                        + "dias para o próximo aniversário!");
                                }
                            }
                            
                            break;

                        case 3:

                            String termo = String.Empty;
                            termo = termo ?? "";
                            var users = _repositoryUsuario.GetAll(termo);
                            String _codigo = "Código";
                            String tituloNome = "Nome";
                            String tituloData = "Data de Nascimento";
                            String titulo = String.Format("\t{0,-8} {1,20} {2,37}\n\n",
                                                           _codigo, tituloNome, tituloData);
                            Console.WriteLine(titulo);

                            for (int i = 0; i < users.Count; i++)
                            {
                                long usuarioId = users[i].Id;
                                String nomeUsuario = users[i].Nome;
                                String sobreNomeUsuario = users[i].SobreNome;
                                String nomeCompleto = nomeUsuario + " " + sobreNomeUsuario;
                                DateTime dNasc = users[i].Nascimento;
                                String dados = String.Format("\t{0,-15} {1,-25} {2,25}\n",
                                                              usuarioId, nomeCompleto, dNasc);
                                Console.WriteLine(dados);
                            }

                            break;

                        case 4:

                            Console.Write("\t\nEntre com o primeiro nome: ");
                            String _nome = Console.ReadLine();

                            //_nome = String.Empty;
                            _nome = _nome ?? "";
                            var nUsers = _repositoryUsuario.GetAll(_nome);
                            String codigo = "Código";
                            String tNome = "Nome";
                            String tData = "Data de Nascimento";
                            String ntitulo = String.Format("\t\n{0,-8} {1,20} {2,37}\n\n",
                                                           codigo, tNome, tData);
                            Console.WriteLine(ntitulo);

                            for (int i = 0; i < nUsers.Count; i++)
                            {
                                long usuarioId = nUsers[i].Id;
                                String nomeUsuario = nUsers[i].Nome;
                                String sobreNomeUsuario = nUsers[i].SobreNome;
                                String nomeCompleto = nomeUsuario + " " + sobreNomeUsuario;
                                DateTime dNasc = nUsers[i].Nascimento;
                                String dados = String.Format("\t{0,-15} {1,-25} {2,20}\n",
                                                              usuarioId, nomeCompleto, dNasc);
                                Console.WriteLine(dados);
                            }

                            Console.WriteLine("\nTem certeza que deseja excluir contato?");
                            Console.Write("\nDigite a chave: ");
                            var chave = long.Parse(Console.ReadLine());
                            _repositoryUsuario.Remove(chave);

                            Console.WriteLine("\t\nUsuário removido com sucesso! ");

                            break;

                        case 5:

                            Console.Write("\t\nEntre com o primeiro nome: ");
                            String name = Console.ReadLine();
                            Console.WriteLine();
                            
                            //_nome = String.Empty;
                            name = name ?? "";
                            var names = _repositoryUsuario.GetAll(name);
                            String _ncodigo = "Código";
                            String _tNome = "Nome";
                            String _tData = "Data de Nascimento";
                            String _ntitulo = String.Format("\t{0,-8} {1,15} {2,35}\n\n",
                                                                _ncodigo, _tNome, _tData);
                            Console.WriteLine(_ntitulo);

                            for (int i = 0; i < names.Count; i++)
                            {
                                long usuarioId = names[i].Id;
                                String nomeUsuario = names[i].Nome;
                                String sobreNomeUsuario = names[i].SobreNome;
                                String nomeCompleto = nomeUsuario + " " + sobreNomeUsuario;
                                DateTime dNasc = names[i].Nascimento;
                                String dados = String.Format("\t{0,-15} {1,-25} {2,20}\n",
                                                              usuarioId, nomeCompleto, dNasc);
                                Console.WriteLine(dados);
                            }

                            Console.WriteLine("\nTem certeza que deseja editar o contato?");
                            Console.Write("\nDigite a chave:");
                            var key = long.Parse(Console.ReadLine());

                            var _usuarioEncontrado = _repositoryUsuario.GetById(key);

                            Console.Write("\nDigite o campo que deseja atualizar: ");
                            var campo = Console.ReadLine();

                            Console.Write("\nDigite o dado que deseja atualizar: ");
                            String dado = Console.ReadLine();

                            var _n = "nome";
                            var _Sn = "sobrenome";
                            var nascimento = "data de nascimento";
                            

                            if ((_n.ToUpper().Contains(campo)) || (_n.Contains(campo)))
                            {
                                _usuarioEncontrado.Nome = dado;
                            }

                            else if ((_Sn.ToUpper().Contains(campo)) || (_Sn.Contains(campo)))
                            {
                                _usuarioEncontrado.SobreNome = dado;
                            }

                            else if ((nascimento.ToUpper().Contains(campo)) || (nascimento.Contains(campo)))
                            {
                                _usuarioEncontrado.Nascimento = DateTime.Parse(dado);
                            }
                            else
                            {
                                Console.WriteLine("Erro! Tente novamente.");
                                break;
                            }
                            
                            var _usuarioAtualizado = _repositoryUsuario.Update(_usuarioEncontrado);

                            Console.Write("\t\nDados do usuário atualizado com sucesso! ");
                            break;

                        case 6:

                            String path = @"C:\Users\Alexander\source\repos\AgendaConsole\
                                                SqlDataBase\DataSource\FileDb\fileDb.txt";

                            var amigos = _repositoryFileDbUsuario.GetDataInFile(path);

                            String titulo_codigo = "Código";
                            String titulo_nome = "Nome";
                            String titulo_data = "Data de Nascimento";
                            String novotitulo = String.Format("\t{0,-8} {1,15} {2,35}\n\n",
                                                                titulo_codigo, titulo_nome, titulo_data);
                            Console.WriteLine(novotitulo);

                            for (int i = 0; i < amigos.Count; i++)
                            {
                                long usuarioId = amigos[i].Id;
                                String nomeUsuario = amigos[i].Nome;
                                String sobreNomeUsuario = amigos[i].SobreNome;
                                String nomeCompleto = nomeUsuario + " " + sobreNomeUsuario;
                                DateTime dNasc = amigos[i].Nascimento;
                                String dados = String.Format("\t{0,-15} {1,-25} {2,25}\n",
                                                              usuarioId, nomeCompleto, dNasc);
                                Console.WriteLine(dados);
                            }
                            break;

                        case 7:

                            String _path = @"C:\Users\Alexander\source\repos\AgendaConsole\
                                                SqlDataBase\DataSource\FileDb\fileDb.txt";
                            String _termo = String.Empty;
                            _termo = _termo ?? "";
                            var _amigos = _repositoryUsuario.GetAll(_termo);
                            _repositoryFileDbUsuario.WriteInFile(_amigos, _path);
                            Console.WriteLine("\nArquivo gravado com sucesso");
                            break;

                        case 0:
                            Console.WriteLine("\t\nEncerrando o programa...");
                            break;

                        default:
                            Console.WriteLine("\t\nOpção inválida!");
                            break;
                    }

                    Console.WriteLine("\t\n\nDigite qualquer tecla para continuar...");
                    Console.ReadLine();
                    if (opcao != 0)
                    {
                        loop = true;
                        Console.Clear();
                    }
                    else
                    {
                        loop = false;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("\t\nTente novamente!");
                    Console.WriteLine("\t\nDigite 'c' para continuar.");
                    String limpaTela = Console.ReadLine();
                    if (limpaTela == "c")
                        Console.Clear();

                }
            }
        }

        public void FindByBirthDayPerson()
        {
            String termo = String.Empty;
            termo = termo ?? "";
            var usuarios = _repositoryUsuario.GetAll(termo);

            Console.WriteLine("\n\tAniversariante(s) do dia!");

            for (int i = 0; i < usuarios.Count; i++)
            {
                var data = usuarios[i].Nascimento;
                usuarios[i].ProxAniv = _calculaDias.CacularDiasAniversario(data);                

                if (usuarios[i].ProxAniv == 0)
                {                    
                    Console.WriteLine($"\n\t{usuarios[i].Nome} {usuarios[i].SobreNome}");
                }                
            }
            
        }
    }
}
