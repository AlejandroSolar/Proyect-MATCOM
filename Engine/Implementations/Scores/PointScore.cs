namespace Engine;

/// <summary>
/// Otorga puntos a los ganadores de un juego en consecuencia con la puntuación de la mano de los perdedores.
/// </summary>
public class PointScore : IScore
{
    public void GrantPrize(IEnumerable<Player> players, IEnumerable<string> winners, Dictionary<string, int> scoreBoard)
    {
        // cantidad de puntos que se otorgará a los ganadores.
        int cantidad = 0;

        // almacena los jugadores que no ganaron.
        var losers = players.Where (player => !winners.Contains(player.Name));

        // suma de las puntuaciones de los perdedores
        cantidad = losers.Sum( player => player.CurrentPoints);


        // los ganadores son premiados con el cociente entre la cantidad final
        // y la cantidad de ganadores.
        foreach (var winner in winners)
        {
            scoreBoard[winner] += cantidad / winners.Count();
        }
    }

    // representación en string del tipo de puntuador.
    public override string ToString()
    {
        return "Puntos por puntuación de los perdedores";
    }
}