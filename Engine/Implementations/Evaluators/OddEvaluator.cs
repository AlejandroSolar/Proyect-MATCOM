namespace Engine;

/// <summary>
/// Se encarga de evaluar solo cuando la suma de sus partes es impar.
/// </summary>
public class OddEvaluator : IEvaluator
{
    // si la suma de las partes izquierda y derecha es impar su valor sera el máximo entre
    // los valores de sus partes, el resto de fichas tendrán de valor por defecto 0.
    public int Evaluate(int left, int right)
    {
        if ((left + right) % 2 != 0)
        {
            return (Math.Max(left, right));
        }

        else return 0;
    }

    // representación en string del tipo de evaluador.
    public override string ToString()
    {
        return "Evaluador de Impares";
    }
}
