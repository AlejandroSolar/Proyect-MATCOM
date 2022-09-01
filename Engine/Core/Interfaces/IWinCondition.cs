namespace Engine;

/// <summary>
/// La interfaz <c>IWinCondition</c> engloba la responsabilidad de establecer los ganadores de un juego.
/// </summary>
public interface IWinCondition
{
    /// <summary>
    /// Establece los ganadores de un juego de acuerdo a cierto criterio.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="IEnumerable"/> de <typeparamref name="string"/> que indica los ganadores del juego.
    /// </returns>
    /// <param name="players"> jugadores que participan en el juego. </param>
    IEnumerable<string> Winners(IEnumerable<Player> players);
}
