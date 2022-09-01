namespace Engine;

/// <summary>
/// Otorga la victoria del juego a los jugadores que se pegaron.
/// </summary>
public class EmptyHandWin : IWinCondition
{
    public IEnumerable<string> Winners(IEnumerable<Player> players)
    {
        // devuelve como ganadores a los jugadores cuya mano es vacía.
        return from x in players where x.Hand.Count() == 0 select x.Name;
    }

    // representación en string del tipo de condición de victoria.
    public override string ToString()
    {
        return "Victoria por pegue";
    }
}