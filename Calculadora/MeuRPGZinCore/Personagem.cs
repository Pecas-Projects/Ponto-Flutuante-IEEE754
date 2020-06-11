using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuRPGZinCore
{

    public class PersonagemEventArgs : EventArgs
    {
        public int NivelAnterior { get; set; }
    }

    public class Personagem
    {
        public delegate void PersonagemUpadoEventHandler(object sender, PersonagemEventArgs e);

        public event PersonagemUpadoEventHandler PersonagemUP;

        public int Nivel { get; set; }
        public int Vida { get; set; }

        private int pontoUpagem = 100;

        public void UparNivel()
        {
            int nivelAnt = this.Nivel;
            Nivel+=2;
            pontoUpagem += 100;
            if(this.PersonagemUP != null)
                this.PersonagemUP(this, new PersonagemEventArgs { NivelAnterior = nivelAnt });
        }

        public void TirarVida()
        {
            Vida--;
        }

        public void GanharVida()
        {
            Vida += 10;
            if(Vida > pontoUpagem)
            {
                UparNivel();
            }
        }
    }
}
