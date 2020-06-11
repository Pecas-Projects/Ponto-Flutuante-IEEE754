using System;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bases = { 2, 16 };
            string input;
            bool negativo = false;
            Console.WriteLine("Digite o número desejado: ");
            input = Console.ReadLine();
            if(input[0] == '-')
            {
                negativo = true;
            }
            string saida = Dec_toBin_32bits(input, negativo);
            Console.WriteLine("O valor em notação ponto flutuante de 32 bits é " + saida);
            saida = Dec_toBin_8bits(input, negativo);
            Console.WriteLine("O valor em notação ponto flutuante de 8 bits é " + saida);
            saida = Dec_toBin_64bits(input, negativo);
            Console.WriteLine("O valor em notação ponto flutuante de 64 bits é " + saida);
        }

        static public string Dec_toBin_8bits(string value, bool negativo) //falta implementar o excesso
        {
            string expoente = null, resultado = null, mantissa = null;
            double valor = Double.Parse(value);
            Console.WriteLine(valor);
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
                mantissa += Math.Floor(fracionaria).ToString();
                multiplicacoes++;
                if (multiplicacoes > 4 || fracionaria == 0)
                {
                    break;
                }
            }

            //completando 4 bits na mantissa
            while (mantissa.Length < 4)
            {
                mantissa += '0';
            }

            if (negativo)
            {
                resultado = "1";
            }
            else
            {
                resultado = "0";
            }

            Console.WriteLine(exp);
            exp += 127; //valor acrescido para o padrão IEEE
            Console.WriteLine(exp);
            expoente = Convert.ToString(Convert.ToByte(exp), 2); //expoente em binário
            if (expoente.Length < 3)
            {
                expoente = '0' + expoente;
            }

            //Console.WriteLine(expoente);
            //Console.WriteLine(mantissa);
            resultado = resultado + expoente + mantissa;

            return resultado;
        }

        //função para converter a parte inteira de decimal para binário
        static public string Dec_toBin_32bits(string value, bool negativo)
        {
            
            string expoente = null, resultado = null, mantissa = null;
            float valor = float.Parse(value);
            Console.WriteLine(valor);
            float fracionaria, proxNum = valor; //proximo numero a ser dividido
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
            else if( valor < 1) //multiplicações sucessivas
            {
                while (true)
                {
                    proxNum = proxNum * 2;
                    exp--;
                    if(proxNum >= 1)
                    {
                        break;
                    }
                }
            }

            //obtendo a parte após a vírgula em binário || 2ª etapa
            fracionaria = proxNum - 1; //obtenção da parte fracionária do quociente
            while (true)
            {
                
                if(fracionaria >= 1)
                {
                    fracionaria -= 1;
                }
                fracionaria *= 2;
                mantissa += Math.Floor(fracionaria).ToString();
                multiplicacoes++;
                if (multiplicacoes > 23 || fracionaria == 0)
                {
                    break;
                }
            }

            //completando 23 bits na mantissa
            while(mantissa.Length < 23)
            {
                mantissa += '0';
            }

            if (negativo)
            {
                resultado = "1";
            }
            else
            {
                resultado = "0";
            }

            //Console.WriteLine(exp);
            exp += 127; //valor acrescido para o padrão IEEE
            //Console.WriteLine(exp);
            expoente = Convert.ToString(Convert.ToByte(exp), 2); //expoente em binário
            if(expoente.Length < 8)
            {
                expoente = '0' + expoente;
            }

            //Console.WriteLine(expoente);
            //Console.WriteLine(mantissa);
            resultado = resultado + expoente + mantissa;

            return resultado;
        }

        static public string Dec_toBin_64bits(string value, bool negativo)
        {

            string expoente = null, resultado = null, mantissa = null;
            double valor = Double.Parse(value);
            Console.WriteLine(valor);
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
                mantissa += Math.Floor(fracionaria).ToString();
                multiplicacoes++;
                if (multiplicacoes > 52 || fracionaria == 0)
                {
                    break;
                }
            }

            //completando 52 bits na mantissa
            while (mantissa.Length < 52)
            {
                mantissa += '0';
            }

            if (negativo)
            {
                resultado = "1";
            }
            else
            {
                resultado = "0";
            }

            //Console.WriteLine(exp);
            exp += 1023; //valor acrescido para o padrão IEEE
            Console.WriteLine(Convert.ToInt64(exp));
            expoente = Convert.ToString(Convert.ToInt64(exp), 2); //expoente em binário
            //procurar um método de conversão para binário de um valor maior do 1000
            if (expoente.Length < 11)
            {
                expoente = '0' + expoente;
            }

            //Console.WriteLine(expoente);
            //Console.WriteLine(mantissa);
            resultado = resultado + expoente + mantissa;

            return resultado;
        }

        //depois, fazer uma função de binário para ponto flutuante
        //função de ponto flutuante (binário) para hexadecimal
    }
}
