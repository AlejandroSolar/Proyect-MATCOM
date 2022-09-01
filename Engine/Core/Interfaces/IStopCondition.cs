namespace Engine;

/// <summary>
/// La interfaz <c>IStopCondition</c> engloba el concepto de detener el juego cuando se cumple determinada condición.
/// </summary>
/// <remarks>
/// Es clonable.
/// </remarks>
public interface IStopCondition : ISimpleClone<IStopCondition>
{
    /// <summary>
    /// Evalua el cumplimiento de una condición determinada para detener el juego.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si el juego debe detenerse.
    /// </returns>
    /// <param name="players"> jugadores que participan en el juego. </param>
    /// <param name="table"> estado actual de la mesa. </param>
    /// <param name="rules"> reglas del juego actual. </param>
    bool Stop(IEnumerable<Player> players, Board table, IEnumerable<IValid> rules);
}
