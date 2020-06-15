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
        public string numeroHexa { get; set; } = null;
        public string Sinal { get; set; } = null;

        public string Expoente8{ get; set; } = null;
        public string Mantissa8 { get; set; } = null;
        public bool arredondado_8bits { get; set; } = false;

        public string Expoente32 { get; set; } = null;
        public string Mantissa32 { get; set; } = null;

        public string Expoente64 { get; set; } = null;
        public string Mantissa64 { get; set; } = null;

        

        public CalculadoraClasse()
        {
            this.numero = 0;
        }

        public void CalculaHexa()
        {
            numeroHexa = null;
            string substring, saida;
            int binario, dec, cont = 0;
            string valor1 = this.Sinal + this.Expoente32 + this.Mantissa32; //formação total do valor em binário com a adição do 0 para ter lenght%4 = 0            
            List<string> values = new List<string>();
            int duracao = 4, inicio = valor1.Length - 4;
            while (inicio >= 0)
            {
                substring = valor1.Substring(inicio, duracao); //pega 4 bits do numero da direita para a esquerda
                binario = int.Parse(substring); //transforma essa subtring em inteiro
                dec = Convert.ToInt32(binario.ToString(), 2); //converte para de decimal para binário os 4 bits
                saida = Convert.ToString(dec, 16); //converte de decimal para hexa                
                values.Insert(0, saida);
                inicio -= 4;
                cont++;
            }
            values.ForEach(delegate (string parte)
            {
                numeroHexa += parte;
            });

        }

        public void Reconstruir() 
        {
            this.Sinal = null;

            this.arredondado_8bits = false;
            this.Expoente8 = null;
            this.Mantissa8 = null;

            this.Expoente32 = null;
            this.Mantissa32 = null;

            this.Expoente64 = null;
            this.Mantissa64 = null;

            this.numeroHexa = null;
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


        public string Calculo_8bits(double valor)
        {
            string expoente = null, resultado = null, mantissa = null, aux = null, fracao;
            valor = DefineSinal(valor);
            double fracionaria;
            bool arredondado = false;
            int exp = 0;

            //pega a parte inteira e converte para binário obtendo uma parte da mantissa

            aux = Math.Floor(valor).ToString();

            fracionaria = valor - Math.Floor(valor);

            if (aux != "0"  && valor <= 225)
            {
                mantissa = Convert.ToString(Convert.ToByte(aux), 2);
            }


            if (mantissa != null) //se o valor for > 1, o expoente é definido pelo tamanho em binário da parte inteira do valor
            {

                if (expoente == "Overflow" || expoente == "Underflow")
                {
                    this.Expoente8 = expoente;
                    return expoente;
                }

                expoente = execesso(mantissa.Length);

            }

            //descorir quantos zeros tem depois da virgula
            else if (mantissa == null)
            {
                fracao = fracionaria.ToString();
                for (int i = 2; i < fracao.Length; ++i) //começa na primeira casa decimal depois da virgula
                {
                    if (fracao[i] == 0) --exp;
                    else break;
                }

                expoente = execesso(exp);
                if (expoente == "Overflow" || expoente == "Underflow") {
                    this.Expoente8 = expoente;
                    return expoente;
                }
            }


            //pega a parte da mantissa que corresponde ao fracionário
            while (true)
            {
                if (fracionaria == 1)
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

            if (mantissa.Length < 4)
            {
                while (mantissa.Length < 4) //se não tiver 4 digitos na mantissa adiciona zeros no final
                {
                    mantissa += "0";
                }
            }

            this.Expoente8 = expoente;
            this.Mantissa8 = mantissa;
            this.arredondado_8bits = arredondado;
            return resultado;
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
