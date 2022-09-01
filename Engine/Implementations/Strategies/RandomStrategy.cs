namespace Engine;

/// <summary>
/// Estrategia que elige una jugada aleatoriamente entre las posibles.
/// </summary>
public class RandomStrategy : IStrategy
{
    public virtual Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // devuelve una jugada aleatoria entre las posibles.
        Random r = new Random();
        return possibles.ElementAt(r.Next(possibles.Count()));
    }

    // representación en string del tipo de estrategia.
    public override string ToString()
    {
        return "Jugador Random";
    }
}