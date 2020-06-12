using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraCore
{
    

    public class CalculadoraClasse
    {
        public double numero { get; set; }
        public string Sinal { get; set; } = null;
        public string Expoente32 { get; set; } = null;
        public string Mantissa32 { get; set; } = null;

        public string Expoente64 { get; set; } = null;
        public string Mantissa64 { get; set; } = null;

        public CalculadoraClasse()
        {
            this.numero = 0;
        }

        public void Reconstruir() 
        {
            this.Sinal = null;
            this.Expoente32 = null;
            this.Mantissa32 = null;

            this.Expoente64 = null;
            this.Mantissa64 = null;
        }


        public double DefineSinal(double num)
        {
            if (num >= 0)
            {
                this.Sinal = "0";
                return num;
            }
            else
            {
                this.Sinal = "1";
                return (num * -1);
            }
        }


        public void Calculo_32bits(double valor)
        {
            valor = DefineSinal(valor);
            double fracionaria, proxNum = valor; //proximo numero a ser dividido
            int exp = 0, multiplicacoes = 0;

            if (valor > 2)
            {
                while (true) //divisões sucessivas
                {
                    proxNum = proxNum / 2;
                    exp++;
                    if (proxNum < 2)
                    {
                        break; //controla quando as divisões pararão
                    }
                }
            }
            else if (valor < 1) //multiplicações sucessivas
            {
                while (true)
                {
                    proxNum = proxNum * 2;
                    exp--;
                    if (proxNum >= 1)
                    {
                        break;
                    }
                }
            }

            //obtendo a parte após a vírgula em binário || 2ª etapa
            fracionaria = proxNum - 1; //obtenção da parte fracionária do quociente
            while (true)
            {

                if (fracionaria >= 1)
                {
                    fracionaria -= 1;
                }
                fracionaria *= 2;
                this.Mantissa32 += Math.Floor(fracionaria).ToString();
                multiplicacoes++;
                if (multiplicacoes > 23 || fracionaria == 0)
                {
                    break;
                }
            }

            exp += 127; //valor acrescido para o padrão IEEE
            //Console.WriteLine(exp);
            this.Expoente32 = Convert.ToString(Convert.ToByte(exp), 2); //expoente em binário
            if (this.Expoente32.Length < 8)
            {
                this.Expoente32 = '0' + this.Expoente32;
            }

        }

        public void Calculo_64bits(double valor)
        {
            valor = DefineSinal(valor);
            double fracionaria, proxNum = valor; //proximo numero a ser dividido
            int exp = 0, multiplicacoes = 0;
            if (valor > 2)
            {
                while (true) //divisões sucessivas
                {
                    proxNum = proxNum / 2;
                    exp++;
                    if (proxNum < 2)
                    {
                        break; //controla quando as divisões pararão
                    }
                }
            }
            else if (valor < 1) //multiplicações sucessivas
            {
                while (true)
                {
                    proxNum = proxNum * 2;
                    exp--;
                    if (proxNum >= 1)
                    {
                        break;
                    }
                }
            }

            //obtendo a parte após a vírgula em binário || 2ª etapa
            fracionaria = proxNum - 1; //obtenção da parte fracionária do quociente
            while (true)
            {

                if (fracionaria >= 1)
                {
                    fracionaria -= 1;
                }
                fracionaria *= 2;
                this.Mantissa64 += Math.Floor(fracionaria).ToString();
                multiplicacoes++;
                if (multiplicacoes > 52 || fracionaria == 0)
                {
                    break;
                }
            }

            //completando 52 bits na mantissa
            while (this.Mantissa64.Length < 52)
            {
                this.Mantissa64 += '0';
            }

           /* if (this.Sinal == "1")
            {
                this.Mantissa64 = "1";
            }
            else
            {
                this.Mantissa64 = "0";
            }*/

            //Console.WriteLine(exp);
            exp += 1023; //valor acrescido para o padrão IEEE
            Console.WriteLine(Convert.ToInt64(exp));
            this.Expoente64 = Convert.ToString(Convert.ToInt64(exp), 2); //expoente em binário
            //procurar um método de conversão para binário de um valor maior do 1000
            if (this.Expoente64.Length < 11)
            {
                this.Expoente64 = '0' + this.Expoente64;
            }



        }

    }
}
