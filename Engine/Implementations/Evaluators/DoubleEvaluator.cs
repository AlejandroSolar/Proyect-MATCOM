namespace Engine;

/// <summary>
/// Se encarga de evaluar las fichas solo cuando son dobles .
/// </summary>
public class DoubleEvaluator : IEvaluator
{
    // si la ficha es un doble su valor sera el de uno de sus lados.
    // el resto de las fichas que no sean dobles tendran valor de 0.
    public int Evaluate(int left, int right)
    {
        if (left == right)
        {
            return (left);
        }
        else return 0;
    }

    // representación en string del tipo de evaluador.
    public override string ToString()
    {
        return "Evaluador de Dobles";
    }
}
