namespace Engine;

/// <summary>
/// Otorga la victoria del juego al jugador con menor puntuación en su mano.
/// </summary>
public class LowerPointsWin : IWinCondition
{
    public IEnumerable<string> Winners(IEnumerable<Player> players)
    {
        // ordena players de menor a mayor y devuelve los jugadores con menos puntos.
        var orderedPlayers = players.OrderBy(x => x.CurrentPoints);
        return from x in orderedPlayers where x.CurrentPoints == orderedPlayers.First().CurrentPoints select x.Name;
    }

    // representación en string del tipo de condición de victoria.
    public override string ToString() => "Victoria por Menor cantidad de puntos";
}