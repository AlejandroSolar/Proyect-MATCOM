namespace Engine;

/// <summary>
/// Otorga la victoria del juego al jugador con mayor puntuación en su mano.
/// </summary>
public class HigherPointsWin : IWinCondition
{
    public IEnumerable<string> Winners(IEnumerable<Player> players)
    {
        // ordena players de menor a mayor y devuelve los jugadores con más puntos.
        var orderedPlayers = players.OrderByDescending(x => x.CurrentPoints);
        return from x in orderedPlayers where x.CurrentPoints == orderedPlayers.First().CurrentPoints select x.Name;
    }

    // representación en string del tipo de condición de victoria.
    public override string ToString()
    {
        return "Victoria por Mayor cantidad de puntos";
    }
}