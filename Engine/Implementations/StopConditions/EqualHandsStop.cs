namespace Engine;

/// <summary>
/// Se encarga de detener el juego cuando todos los jugadores tienen la misma puntuación en su mano.
/// </summary>
public class EqualHandsStop : IStopCondition
{
    public bool Stop(IEnumerable<Player> players, Board mesa, IEnumerable<IValid> reglas)
    {
        // recorre los jugadores
        for (int i = 0; i < players.Count() - 1; i++)
        {
            // si alguno de los jugadores tiene una puntuación en su mano distinta a la del siguiente
            // el juego continua.
            if (players.ElementAt(i).CurrentPoints != players.ElementAt(i + 1).CurrentPoints)
            {
                return false;
            }
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
