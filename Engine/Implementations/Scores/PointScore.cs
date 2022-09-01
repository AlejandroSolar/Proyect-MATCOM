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

        // recorre los jugadores.
        for (int i = 0; i < players.Count(); i++)
        {
            // si un jugador no es ganador añade sus puntos a la cantidad final.
            if (!winners.Contains(players.ElementAt(i).Name))
            {
                cantidad += players.ElementAt(i).CurrentPoints;
            }
        }

        // los ganadores son premiados con el cociente entre la cantidad final
        // y la cantidad de ganadores.
        for (int i = 0; i < winners.Count(); i++)
        {
            scoreBoard[winners.ElementAt(i)] += cantidad / winners.Count();
        }
    }

    // representación en string del tipo de puntuador.
    public override string ToString()
    {
        return "Puntos por puntuación de los perdedores";
    }
}