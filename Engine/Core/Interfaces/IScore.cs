namespace Engine;

/// <summary>
/// La interfaz <c>IScore</c> engloba la responsabilidad de premiar la victoria de un juego en un torneo.
/// </summary>
public interface IScore
{
    /// <summary>
    /// Actualiza la tabla de posiciones de acuerdo con los ganadores de cada juego.
    /// </summary>
    /// <param name="players"> jugadores del torneo. </param>
    /// <param name="winners"> ganadores del torneo. </param>
    /// <param name="scoreBoard"> tabla de posiciones del torneo. </param>
    void GrantPrize(IEnumerable<Player> players, IEnumerable<string> winners, Dictionary<string, int> scoreBoard);
}
