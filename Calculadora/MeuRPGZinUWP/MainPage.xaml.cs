using System;
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
using MeuRPGZinCore;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace MeuRPGZinUWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Personagem p = null;
        
        public MainPage()
        {
            this.InitializeComponent();
            BtnCriarDoar.Click += BtnCriarDoar_Click;            
        }

        private void BtnCriarDoar_Click(object sender, RoutedEventArgs e)
        {
            if (p == null)
            {
                p = new Personagem { Nivel = 1, Vida = 10 };
                var btn = sender as Button;
                btn.Content = "Doar Vida";
                p.PersonagemUP += TratarPersonagemUPLevel;
                TblLevel.Text = "Nível:" + p.Nivel;
                TblLife.Text = "Vida:" + p.Vida;
            }
            else
            {
                p.GanharVida();
                TblLife.Text = "Vida:" + p.Vida;
            }
        }

        private void TratarPersonagemUPLevel(object sender, PersonagemEventArgs e)
        {
            
            var persona = sender as MeuRPGZinCore.Personagem;
            TblLevel.Text = "Nível Anterior: " + e.NivelAnterior + " Novo Nível: " + persona.Nivel; 
        }

        private void BtnRetirar_Click(object sender, RoutedEventArgs e)
        {
            p?.TirarVida();
            TblLife.Text = "Vida:" + p?.Vida;
        }

        private void BtnVai_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pagina2), p);
        }

        //private void MeuBotao_Click(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    if (btn != null)
        //    {
        //        double topBotao = Canvas.GetTop(btn);

        //        if (btn.Content.ToString() == "TÓXICO")
        //        {
        //            Debug.WriteLine("EU SOU TÓXICO!");
        //        }
        //    }
        //}

        //private void MeuBotao_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    MeuBotao.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

        //}

        //private void MeuBotao_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    // MeuBotao.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
        //}
    }
}
