namespace Engine;

/// <summary>
/// Se encarga de evaluar solo cuando son la suma de sus partes es par.
/// </summary>
public class EvenEvaluator : IEvaluator
{
    // si la suma de sus partes izquierda y derecha es par su valor sera su media.
    // el resto de fichas que no cumplan con dicha condición tendrán valor 0.
    public int Evaluate(int left, int right)
    {
        if ((left + right) % 2 == 0)
        {
            return ((left + right) / 2);
        }

        else return 0;
    }

    // representación en string del tipo de evaluador.
    public override string ToString() => "Evaluador de Pares";
}