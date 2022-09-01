namespace Engine;

/// <summary>
/// La interfaz <c>IEvaluator</c> engloba la responsabilidad de asignarle un valor a las fichas. 
/// </summary>
public interface IEvaluator
{
    /// <summary>
    /// Asigna el valor a la ficha en correspondencia con los valores de sus partes izquierda y derecha.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="int"/> con el valor asignado a la ficha.
    /// </returns>
    /// <param name="left"> parte izquierda de la ficha a evaluar. </param>
    /// <param name="right"> parte derecha de la ficha a evaluar. </param>
    int Evaluate(int left, int right);
}
