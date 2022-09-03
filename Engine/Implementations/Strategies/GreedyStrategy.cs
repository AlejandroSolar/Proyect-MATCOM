namespace Engine;

/// <summary>
/// Estrategia que prioriza jugar las fichas de mayor valor.
/// </summary>
public class GreedyStrategy : IStrategy
{
    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // ordena las jugadas posibles y devuelve la que tiene la ficha de mayor valor.
        return possibles.OrderByDescending(x => x.Piece.PieceValue).First();
    }

    // representación en string del tipo de estrategia.
    public override string ToString() => "Jugador Botagorda";
}