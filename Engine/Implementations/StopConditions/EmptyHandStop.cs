namespace Engine;

/// <summary>
/// Se encarga de detener el juego cuando uno de los jugadores se pega.
/// </summary>
public class EmptyHandStop : IStopCondition
{
    public bool Stop(IEnumerable<Player> players, Board table, IEnumerable<IValid> rules)
    {
        // recorre los jugadores.
        for (int i = 0; i < players.Count(); i++)
        {
            // si la mano de algún jugador está vacía entonces indica que se pegó,
            // luego el juego debe detenerse.
            if (players.ElementAt(i).Hand?.Count == 0)
            {
                return true;
            }
        }

        // en caso contrario el juego continúa.
        return false;
    }

    // representación en string del tipo de condición de parada.
    public override string ToString()
    {
        return "Parada por pegue de un Jugador";
    }
}
