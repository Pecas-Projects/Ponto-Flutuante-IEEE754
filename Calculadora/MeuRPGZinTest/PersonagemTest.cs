using MeuRPGZinCore;
using NUnit.Framework;

namespace MeuRPGZinTest
{
    public class Tests
    {
        Personagem p;

        [SetUp]
        public void Setup()
        {
            p = new Personagem { Vida = 10, Nivel = 1 };            
        }

        [Test]
        public void LevelUp()
        {
            p.Vida = 99;
            p.GanharVida();
            Assert.AreEqual(3, p.Nivel);
        }
    }
}