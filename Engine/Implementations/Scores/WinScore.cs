namespace Engine;

/// <summary>
/// Otorga un punto a los ganadores de un juego.
/// </summary>
public class WinScore : IScore
{
    public void GrantPrize(IEnumerable<Player> players, IEnumerable<string> winners, Dictionary<string, int> scoreBoard)
    {
        // recorre los ganadores.
        for (int i = 0; i < winners.Count(); i++)
        {
            // a cada ganador de un juego se le suma 1 pto por ganar.
            scoreBoard[winners.ElementAt(i)] += 1;
        }
    }

    // representación en string del tipo de puntuador.
    public override string ToString()
    {
        return "Puntos por Victoria";
    }
}