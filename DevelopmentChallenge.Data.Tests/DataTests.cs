using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    [TestFixture]
    public class DataTests
    {
        [TestCase]
        public void TestResumenListaVacia()
        {
            Assert.AreEqual("<h1>Lista vacía de formas!</h1>",
                FormaGeometrica.Imprimir(new List<IFormaGeometrica>(), 1));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnIngles()
        {
            Assert.AreEqual("<h1>Empty list of shapes!</h1>",
                FormaGeometrica.Imprimir(new List<IFormaGeometrica>(), 2));
        }

        [TestCase]
        public void TestResumenListaConUnCuadrado()
        {
            var cuadrados = new List<IFormaGeometrica> { new Cuadrado(5) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, Idioma.Castellano);

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadrados()
        {
            var cuadrados = new List<IFormaGeometrica> { 
                new Cuadrado(5), 
                new Cuadrado(1), 
                new Cuadrado(3) 
            };

            var resumen = FormaGeometrica.Imprimir(cuadrados, Idioma.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTipos()
        {
            var formas = new List<IFormaGeometrica> {            
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = FormaGeometrica.Imprimir(formas, Idioma.Ingles);

            Assert.AreEqual(
                "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 13,01 | Perimeter 18,06 <br/>3 Triangles | Area 49,64 | Perimeter 51,6 <br/>TOTAL:<br/>7 shapes Perimeter 97,66 Area 91,65",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnCastellano()
        {
            var formas = new List<IFormaGeometrica> {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = FormaGeometrica.Imprimir(formas, Idioma.Castellano);

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 Círculos | Area 13,01 | Perimetro 18,06 <br/>3 Triángulos | Area 49,64 | Perimetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 97,66 Area 91,65",
                resumen);
        }

        // NUEVOS TEST 

        [TestCase]
        public void TestResumenListaVaciaFormasEnItaliano()
        {
            Assert.AreEqual("<h1>Elenco vuoto di forme!</h1>",
                FormaGeometrica.Imprimir(new List<IFormaGeometrica>(), Idioma.Italiano));
        }

        [TestCase]
        public void TestResumenListaConUnCuadradoEnItaliano()
        {
            var cuadrados = new List<IFormaGeometrica> { new Cuadrado(5) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, Idioma.Italiano);

            Assert.AreEqual("<h1>Rapporto sulle forme</h1>1 Quadrato | Area 25 | Perimetro 20 <br/>TOTALE:<br/>1 forme Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnTrapecioEnItaliano()
        {
            var cuadrados = new List<IFormaGeometrica> { new Trapecio(10, 5, 4, 4) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, Idioma.Italiano);

            Assert.AreEqual("<h1>Rapporto sulle forme</h1>1 Trapezio | Area 30 | Perimetro 23 <br/>TOTALE:<br/>1 forme Perimetro 23 Area 30", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadradosEnItaliano()
        {
            var cuadrados = new List<IFormaGeometrica> {
                new Cuadrado(5),
                new Cuadrado(1),
                new Cuadrado(3)
            };

            var resumen = FormaGeometrica.Imprimir(cuadrados, Idioma.Italiano);

            Assert.AreEqual("<h1>Rapporto sulle forme</h1>3 Quadrati | Area 35 | Perimetro 36 <br/>TOTALE:<br/>3 forme Perimetro 36 Area 35", resumen);
        }


        [TestCase]
        public void TestResumenListaConMasTiposEnItaliano()
        {
            var formas = new List<IFormaGeometrica> {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m),
                new Trapecio(10,5,4,4)
            };

            var resumen = FormaGeometrica.Imprimir(formas, Idioma.Italiano);

            Assert.AreEqual(
                "<h1>Rapporto sulle forme</h1>2 Quadrati | Area 29 | Perimetro 28 <br/>2 Cerchi | Area 13,01 | Perimetro 18,06 <br/>3 Triangoli | Area 49,64 | Perimetro 51,6 <br/>1 Trapezio | Area 30 | Perimetro 23 <br/>TOTALE:<br/>8 forme Perimetro 120,66 Area 121,65",
                resumen);
        }
    }
}
