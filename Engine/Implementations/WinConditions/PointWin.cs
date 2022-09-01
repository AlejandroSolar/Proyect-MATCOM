namespace Engine;

/// <summary>
/// Otorga la victoria del juego a los jugadores que tengan
/// un valor total en su mano acorde con determinado criterio.
/// </summary>
public abstract class PointWin : IWinCondition
{
    // valor con el que deben compararse las manos de los jugadores.
    protected int Points { get; set; }

    public virtual IEnumerable<string> Winners(IEnumerable<Player> players)
    {
        //lista de ganadores a devolver.
        IList<string> winners = new List<string>();

        for (int i = 0; i < players.Count(); i++)
        {
            // si los puntos de la mano del jugador coinciden con el valor de points
            // se le otorga la victoria a dicho jugador.
            if (players.ElementAt(i).CurrentPoints == Points)
            {
                winners.Add(players.ElementAt(i).Name);
            }
        }

        // retorna los ganadores.
        return winners;
    }

    // representación en string del tipo de condición de victoria.
    public abstract override string ToString();
}