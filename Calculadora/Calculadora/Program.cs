using System;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bases = { 2, 16 };
            string input, saida;
            bool negativo = false;
            Console.WriteLine("Digite o número desejado: ");
            input = Console.ReadLine();
            if(input[0] == '-')
            {
                negativo = true;
            }
           /* string saida = Dec_toBin_32bits(input, negativo);
            Console.WriteLine("O valor em notação ponto flutuante de 32 bits é " + saida);
            saida = Dec_toBin_8bits(input);
            Console.WriteLine("O valor em notação ponto flutuante de 8 bits é " + saida);
            saida = Dec_toBin_64bits(input, negativo);
            Console.WriteLine("O valor em notação ponto flutuante de 64 bits é " + saida); */
            
            saida = Dec_toBin_8bits(input);
            Console.WriteLine("O valor em notação ponto flutuante de 8 bits é " + saida);


        }

        static public string execesso(int expoente)
        {
            string resultado = null;

            if (expoente == -4) resultado = "000";
            else if (expoente == -3) resultado = "001";
            else if (expoente == -2) resultado = "010";
            else if (expoente == -1) resultado = "011";
            else if (expoente == 0) resultado = "100";
            else if (expoente == 1) resultado = "101";
            else if (expoente == 2) resultado = "110";
            else if (expoente == 3) resultado = "111";
            else if (expoente > 3) resultado = "Overflow";
            else if (expoente < -1) resultado = "Underflow";

            return resultado;
        }

        static public string Dec_toBin_8bits(string value) //falta implementar o excesso
        {
            string expoente = null, resultado = null, mantissa = null, aux = null, fracao;
            double valor = Double.Parse(value);
            Console.WriteLine(valor);
            double fracionaria;
            bool arredondado = false;
            int exp = 0;

            //pega a parte inteira e converte para binário obtendo uma parte da mantissa
            
            aux = Math.Floor(valor).ToString();

            fracionaria = valor - Math.Floor(valor);

            if(aux != "0")
            {
                mantissa = Convert.ToString(Convert.ToByte(aux), 2);
            }
            
            
            if(mantissa != null) //se o valor for > 1, o expoente é definido pelo tamanho em binário da parte inteira do valor
            {
                expoente = execesso(mantissa.Length);
                if (expoente == "Overflow" || expoente == "Underflow") return expoente;
                
            }

            //descorir quantos zeros tem depois da virgula
            else if (mantissa == null)
            {
                fracao = fracionaria.ToString();
                for (int i = 2; i < fracao.Length ; ++i) //começa na primeira casa decimal depois da virgula
                {
                    if (fracao[i] == 0) --exp;
                    else break;
                }

                expoente = execesso(exp);
                if (expoente == "Overflow" || expoente == "Underflow") return expoente;
            }


            //pega a parte da mantissa que corresponde ao fracionário
            while (true)
            {
                if (fracionaria == 0)
                {
                    break;
                }

                if (mantissa != null && mantissa.Length == 4)
                {
                    arredondado = true;
                    break;
                }

                if (fracionaria >= 1)
                {
                    fracionaria -= 1;
                }
                fracionaria *= 2;
                mantissa += Math.Floor(fracionaria).ToString();
               
               
            }

            if(mantissa.Length < 4)
            {
                while(mantissa.Length < 4) //se não tiver 4 digitos na mantissa adiciona zeros no final
                {
                    mantissa += "0";
                }
            }

            resultado = "0"+ expoente + mantissa;
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

    }
}
