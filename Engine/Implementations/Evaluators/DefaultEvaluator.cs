namespace Engine;

/// <summary>
/// Se encarga de evaluar las fichas de la forma clasica (parte izquierda + parte derecha).
/// </summary>
public class DefaultEvaluator : IEvaluator
{
    // el valor de la ficha va a ser la suma de los valores de sus partes izquierda y derecha.
    public int Evaluate(int left, int right) => left + right;

    // representación en string del tipo de evaluador.
    public override string ToString() => "Evaluador Clasico";
}