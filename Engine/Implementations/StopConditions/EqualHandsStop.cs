namespace Engine;

/// <summary>
/// Se encarga de detener el juego cuando todos los jugadores tienen la misma puntuación en su mano.
/// </summary>
public class EqualHandsStop : IStopCondition
{
    public bool Stop(IEnumerable<Player> players, Board mesa, IEnumerable<IValid> reglas)
    {
        // cantidad de puntos que debe tener cada jugador.
        int points = players.First().CurrentPoints;

        // si alguno de los jugadores no tiene la cantidad indicada en points.
        // no detiene el juego.
        if (players.Any(player => player.CurrentPoints != points))
        {
            return false;
        }
        // si todos tienen la misma puntuación el juego debe detenerse.
        return true;
    }

    // representación en string del tipo de condición de parada.
    public override string ToString()
    {
        return "Parada por igualdad de puntos";
    }
}
