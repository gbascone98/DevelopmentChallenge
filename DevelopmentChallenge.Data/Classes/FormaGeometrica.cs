/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

public static class Idioma
{
    public const int Castellano = 1; // Constante para representar el idioma Castellano.
    public const int Ingles = 2; // Constante para representar el idioma Inglés.
    public const int Italiano = 3; // Constante para representar el idioma Italiano.

    // ResourceManager para manejar los recursos de traducción.
    public static ResourceManager ResourceManager = new ResourceManager("DevelopmentChallenge.Data.Classes.Idiomas.Recursos", Assembly.GetExecutingAssembly());

    // Método para obtener la traducción de una clave específica en el idioma seleccionado.
    public static string ObtenerTraduccion(string key, int idioma)
    {
        var culture = ObtenerCulture(idioma); // Obtiene el CultureInfo correspondiente al idioma.
        return ResourceManager.GetString(key, culture); // Devuelve la traducción de la clave.
    }

    // Método privado para obtener el CultureInfo correspondiente a un idioma.
    private static CultureInfo ObtenerCulture(int idioma)
    {
        switch (idioma)
        {
            case Castellano:
                return new CultureInfo("es"); // Retorna CultureInfo para Español.
            case Ingles:
                return new CultureInfo("en"); // Retorna CultureInfo para Inglés.
            case Italiano:
                return new CultureInfo("it"); // Retorna CultureInfo para Italiano.
            default:
                throw new NotImplementedException(); // Lanza una excepción si el idioma no está implementado.
        }
    }
}
public interface IFormaGeometrica
{
    decimal CalcularArea(); // Método para calcular el área de la forma geométrica.
    decimal CalcularPerimetro(); // Método para calcular el perímetro de la forma geométrica.
    string ObtenerNombre(int cantidad, int idioma); // Método para obtener el nombre de la forma en función de la cantidad y el idioma.
}

public class Cuadrado : IFormaGeometrica
{
    private readonly decimal _lado;

    public Cuadrado(decimal lado)
    {
        _lado = lado;
    }

    public decimal CalcularArea() => _lado * _lado;
    public decimal CalcularPerimetro() => _lado * 4;

    public string ObtenerNombre(int cantidad, int idioma)
    {
        return cantidad == 1 ? Idioma.ObtenerTraduccion("SquareSingular", idioma) : Idioma.ObtenerTraduccion("SquarePlural", idioma);
    }
}

public class Circulo : IFormaGeometrica
{
    private readonly decimal _radio;

    public Circulo(decimal radio)
    {
        _radio = radio;
    }

    public decimal CalcularArea() => (decimal)Math.PI * (_radio / 2) * (_radio / 2);
    public decimal CalcularPerimetro() => (decimal)Math.PI * _radio;

    public string ObtenerNombre(int cantidad, int idioma)
    {
        return cantidad == 1 ? Idioma.ObtenerTraduccion("CircleSingular", idioma) : Idioma.ObtenerTraduccion("CirclePlural", idioma);
    }
}

public class TrianguloEquilatero : IFormaGeometrica
{
    private readonly decimal _lado;

    public TrianguloEquilatero(decimal lado)
    {
        _lado = lado;
    }

    public decimal CalcularArea() => ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
    public decimal CalcularPerimetro() => _lado * 3;

    public string ObtenerNombre(int cantidad, int idioma)
    {
        return cantidad == 1 ? Idioma.ObtenerTraduccion("TriangleSingular", idioma) : Idioma.ObtenerTraduccion("TrianglePlural", idioma);
    }
}

public class Trapecio : IFormaGeometrica
{
    private readonly decimal _baseMayor;
    private readonly decimal _baseMenor;
    private readonly decimal _altura;
    private readonly decimal _lado;

    public Trapecio(decimal baseMayor, decimal baseMenor, decimal altura, decimal lado)
    {
        _baseMayor = baseMayor;
        _baseMenor = baseMenor;
        _altura = altura;
        _lado = lado;
    }

    public decimal CalcularArea() => (_baseMayor + _baseMenor) * _altura / 2;
    public decimal CalcularPerimetro() => _baseMayor + _baseMenor + 2 * _lado;

    public string ObtenerNombre(int cantidad, int idioma)
    {
        return cantidad == 1 ? Idioma.ObtenerTraduccion("TrapezoidSingular", idioma) : Idioma.ObtenerTraduccion("TrapezoidPlural", idioma);
    }
}

public static class FormaGeometrica
{
    // Diccionario que almacena ejemplos de cada tipo de forma geométrica.
    private static readonly Dictionary<Type, IFormaGeometrica> FormaEjemplos = new Dictionary<Type, IFormaGeometrica>
    {
        { typeof(Cuadrado), new Cuadrado(1) },
        { typeof(Circulo), new Circulo(1) },
        { typeof(TrianguloEquilatero), new TrianguloEquilatero(1) },
        { typeof(Trapecio), new Trapecio(1, 1, 1, 1) }
    };

    // Método para imprimir el reporte de formas geométricas en el idioma seleccionado.
    public static string Imprimir(List<IFormaGeometrica> formas, int idioma)
    {
        var sb = new StringBuilder();

        if (!formas.Any())
        {
            // Si no hay formas, imprime el encabezado para lista vacía.
            sb.Append(GetCabecera(idioma, true));
        }
        else
        {
            // Imprime el encabezado para el reporte de formas.
            sb.Append(GetCabecera(idioma, false));

            // Agrupa las formas por tipo y calcula el resumen de áreas y perímetros.
            var resumenFormas = formas.GroupBy(f => f.GetType())
                                      .Select(g => new
                                      {
                                          Tipo = g.Key,
                                          Cantidad = g.Count(),
                                          Area = g.Sum(f => f.CalcularArea()),
                                          Perimetro = g.Sum(f => f.CalcularPerimetro())
                                      }).ToList();

            foreach (var resumen in resumenFormas)
            {
                var forma = FormaEjemplos[resumen.Tipo];
                // Agrega cada resumen de forma al StringBuilder.
                sb.Append(resumen.Cantidad + " " + forma.ObtenerNombre(resumen.Cantidad, idioma) + " | " + Idioma.ObtenerTraduccion("Area", idioma) + " " + resumen.Area.ToString("#.##") + " | " + Idioma.ObtenerTraduccion("Perimeter", idioma) + " " + resumen.Perimetro.ToString("#.##") + " <br/>");
            }

            // Calcula los totales de formas, áreas y perímetros.
            var totalFormas = resumenFormas.Sum(r => r.Cantidad);
            var totalPerimetro = resumenFormas.Sum(r => r.Perimetro);
            var totalArea = resumenFormas.Sum(r => r.Area);

            // Agrega los totales al StringBuilder.
            sb.Append(Idioma.ObtenerTraduccion("Total", idioma) + ":<br/>" + totalFormas + " " + Idioma.ObtenerTraduccion("Shapes", idioma) + " ");
            sb.Append(Idioma.ObtenerTraduccion("Perimeter", idioma) + " " + totalPerimetro.ToString("#.##") + " ");
            sb.Append(Idioma.ObtenerTraduccion("Area", idioma) + " " + totalArea.ToString("#.##"));
        }

        return sb.ToString(); // Devuelve el reporte como string.
    }

    // Método privado para obtener el encabezado del reporte basado en si la lista está vacía o no.
    private static string GetCabecera(int idioma, bool isEmpty)
    {
        return isEmpty ? "<h1>" + Idioma.ObtenerTraduccion("EmptyListHeader", idioma) + "</h1>" : "<h1>" + Idioma.ObtenerTraduccion("ShapesReportHeader", idioma) + "</h1>";
    }
}


