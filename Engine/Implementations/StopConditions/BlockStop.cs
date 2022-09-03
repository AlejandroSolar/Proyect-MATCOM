namespace Engine;

/// <summary>
/// Se encarga de detener el juego cuando ningun jugador puede jugar con las fichas que tiene actualmente.
/// </summary>
public class BlockStop : IStopCondition
{

    public bool Stop(IEnumerable<Player> players, Board table, IEnumerable<IValid> reglas)
    {
        // verifica si algún jugador jugó correctamente en la ronda.
        if(players.Any( player => !Round.SkipTurn(player,reglas,table)))
        {
            return false;
        }

        //en caso contrario el juego se detiene.
        return true;
    }

    // representación en string del tipo de condición de parada.
    public override string ToString() => "Parada por tranque del juego";
}
