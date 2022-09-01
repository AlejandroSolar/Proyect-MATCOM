namespace Engine;

/// <summary>
/// Organiza los turnos de tal forma que queden organizados de mayor a menor teniendo
/// en cuenta el valor de las manos de los jugadores.
/// </summary>
public class ValueOrderer: ITurnOrderer
{
    public void Organize(IList<Player> players, IEnumerable<Couple> couples)
    {
        //recorre los jugadores 
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i; j < players.Count - 1; j++)
            {
                // si el valor de la mano de un jugador es menor que el valor
                // de la mano del siguiente jugador los intercambia de lugar.
                if (players[i].CurrentPoints < players[j].CurrentPoints)
                {
                    Swap(players, i, j);
                }
            }
        }
    }

    /// <summary>
    /// Intercambia las posiciones de 2 jugadores en la lista de jugadores.
    /// </summary>
    /// <param name="playersList"> lista de jugadores. </param>
    /// <param name="a"> indice del primer jugador. </param>
    /// <param name="b"> indice del segundo jugador. </param>
    private void Swap(IList<Player> playersList, int a, int b)
    {
        Player temp = playersList[a];
        playersList[a] = playersList[b];
        playersList[b] = temp;
    }

    // representación en string del tipo de ordenador de turnos.
    public override string ToString()
    {
        return "Ordenador de turnos por valor";
    }
}