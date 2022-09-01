namespace Engine;

/// <summary>
/// La interfaz <c>ITurnOrderer</c> engloba la responsabilidad de establecer el orden en los que juegan los jugadores.
/// </summary>
public interface ITurnOrderer
{
    /// <summary>
    /// Organiza el listado de jugadores de acuerdo con un criterio dado.
    /// </summary>
    /// <param name="players"> jugadores que participan en el juego. </param>
    /// <param name="couples"> parejas que participan en el juego. </param>
    void Organize(IList<Player> players, IEnumerable<Couple> couples);
}
