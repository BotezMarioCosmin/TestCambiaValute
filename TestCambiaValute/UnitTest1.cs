using LibreriaMacchinaCambiaValute;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace TestCambiaValute
{
    public class UnitTest1
    {
        MacchinaCambiaValute m;
        [Fact]
        public void CreazioneOggetto()
        {
            m = new MacchinaCambiaValute();
            Assert.True(m != null);
        }

        [Fact]
        public void CaricaImportoMaggioreDiZero()
        {
            m = new MacchinaCambiaValute();
            m.Carica(10, "€");
            Assert.True(m.Importo == 10);
        }

        [Fact]
        public void CaricaImportoUgualeAZero()
        {
            m = new MacchinaCambiaValute();
            Assert.Throws<Exception>(() => m.Carica(0, "€"));
        }

        [Fact]
        public void CaricaImportoNegativo()
        {
            m = new MacchinaCambiaValute();
            Assert.Throws<Exception>(() => m.Carica(-10, "€"));
        }

        [Fact]
        public void CaricaImportoConValutaNonValida()
        {
            m = new MacchinaCambiaValute();
            Assert.Throws<Exception>(() => m.Carica(10, "a"));
        }

        [Fact]
        public void ConvertiSenzaCaricareImporto()
        {
            m = new MacchinaCambiaValute();
            Assert.Throws<Exception>(() => m.Converti("$"));
        }

        [Fact]
        public void Converti()
        {
            m = new MacchinaCambiaValute();
            m.Carica(1,"€");
            m.Converti("£");
            Assert.True(m.Importo == 0.89);
        }

        [Fact]
        public void ConvertiValutaNonValida()
        {
            m = new MacchinaCambiaValute();
            m.Carica(1, "€");
            Assert.Throws<Exception>(() => m.Converti("¥"));
        }

        [Fact]
        public void ConvertiArrotondato()
        {
            m = new MacchinaCambiaValute();
            m.Carica(0.99, "€");
            Assert.True(m.Converti("€") != 1);
        }

        [Fact]
        public void toString()
        {
            m = new MacchinaCambiaValute("id","ditta","12/12/2020");
            Assert.True(m.ToString != null);
        }

        [Fact]
        public void ErogaImportoCorretto()
        {
            m = new MacchinaCambiaValute();
            m.Carica(1,"€");
            m.Converti("€");
            Assert.True(m.Eroga() == "1 €");
        }

        [Fact]
        public void ErogaResettaImporto()
        {
            m = new MacchinaCambiaValute();
            m.Carica(1, "€");
            m.Converti("£");
            m.Eroga();
            Assert.True(m.Importo == 0);
        }

        [Fact]
        public void CloneEdEquals()
        {
            m = new MacchinaCambiaValute("id","ditta");
            MacchinaCambiaValute m2;
            m2 = m.Clone();
            Assert.True(m2.Equals(m));
        }

        [Fact]
        public void ContaErogzioni()
        {
            m = new MacchinaCambiaValute("id", "ditta");
            m.Carica(12,"€");
            m.Eroga();
            m.Carica(2, "$");
            m.Eroga();
            m.Carica(1, "£");
            m.Eroga();
            Assert.True(m.ContaErogazioni == 3);
        }
    }
}