using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class GestorDiasAniversario : ICalculaDiasAniversario
    {
        public int CacularDiasAniversario(DateTime DateOfBirth)
        {
            DateTime dataProxAniv = new DateTime(DateTime.Now.Year, DateOfBirth.Month, DateOfBirth.Day);
            DateTime dataAtual = DateTime.Now;
            TimeSpan diasProxAniv = dataProxAniv.Subtract(dataAtual);

            if (dataProxAniv > dataAtual)
            {
                var DiasProxAniv = diasProxAniv.TotalDays;
                return (int)DiasProxAniv + 1;
            }
            else if (diasProxAniv.Days == 0)
            {
                var DiasProxAniv = diasProxAniv.Days;
                return DiasProxAniv;
            }
            else
            {
                var qtdDias = diasProxAniv.Days;
                var DiasProxAniv = qtdDias + 365;
                return DiasProxAniv;
            }
        }
    }
}
