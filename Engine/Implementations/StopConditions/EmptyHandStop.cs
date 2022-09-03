namespace Engine;

/// <summary>
/// Se encarga de detener el juego cuando uno de los jugadores se pega.
/// </summary>
public class EmptyHandStop : IStopCondition
{
    public bool Stop(IEnumerable<Player> players, Board table, IEnumerable<IValid> rules)
    {
        // verifica si algún jugador se pegó.
        if(players.Any(player => player.Hand.Count == 0))
        {
            return true;
        }
        
        // en caso contrario el juego continúa.
        return false;
    }

    // representación en string del tipo de condición de parada.
    public override string ToString() => "Parada por pegue de un Jugador";
}
