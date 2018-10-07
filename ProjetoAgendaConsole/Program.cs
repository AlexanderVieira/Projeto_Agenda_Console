using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgendaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.PrintMenu();
            //Console.ReadLine();
        }
    }
}
