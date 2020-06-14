﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CalculadoraCore;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace CalculadoraUWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string valor;
        public CalculadoraClasse Calculadora = new CalculadoraClasse();

        public MainPage()
        {
            this.InitializeComponent();  
        }

        public void AtualizaCalculos()
        {
            
            if(Calculadora.Expoente8 != null && Calculadora.Mantissa8 != null)
            {
                if (Calculadora.Expoente8 != "Overflow" && Calculadora.Expoente8 != "Underflow")
                {
                    sinalCalculo_8.Text = Calculadora.Sinal;
                    mantissaCalculo_8.Text = Calculadora.Mantissa8;

                }
                expoenteCalculo_8.Text = Calculadora.Expoente8;             
            }
            

            sinalCalculo_32.Text = Calculadora.Sinal;
            expoenteCalculo_32.Text = Calculadora.Expoente32;
            mantissaCalculo_32.Text = Calculadora.Mantissa32;

            sinalCalculo_64.Text = Calculadora.Sinal;
            sinalCalculo_64.Text = Calculadora.Sinal;
            expoenteCalculo_64.Text = Calculadora.Expoente64;
            mantissaCalculo_64.Text = Calculadora.Mantissa64;
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Calcular_btn(object sender, RoutedEventArgs e)
        {
            valor = valorDigitado.Text;
            if(valor.IndexOf('.') != -1)
            {
                valor = valor.Replace('.', ',');
            }

            double valorDouble = Double.Parse(valor);

            if(valorDouble != 0)
            {
                if ( valorDouble <= 225)
                {
                    Calculadora.Calculo_8bits(valorDouble);
                }
                else
                {
                    Calculadora.Mantissa8 = "Valor muito grande";
                }
                
                Calculadora.Calculo_32bits(valorDouble);
                Calculadora.Calculo_64bits(valorDouble);

                if (Calculadora.arredondado_8bits == true)
                {
                    Calculadora.Mantissa8 += " (arredondado)";
                }

                if (Calculadora.Mantissa32.Count() < 23)
                {
                    for (int a = Calculadora.Mantissa32.Count(); a <= 23; a++)
                    {
                        Calculadora.Mantissa32 += "0";
                    }
                }

                AtualizaCalculos();
                Calculadora.Reconstruir();
            }

        }

        private void mantissaCalculo_64_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
