namespace Engine;

/// <summary>
/// Estrategia que prioriza jugar las fichas que son dobles.
/// </summary>
public class DoublesStrategy : IStrategy
{
    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // recorre la lista de jugadas posibles
        foreach (var move in possibles)
        {
            // si la ficha en possibles en el indice i es doble devuelve esa jugada.
            if (move.Piece.LeftSide == move.Piece.RightSide)
            {
                return move;
            }
        }
        // si no encuentra dobles retorna la primera jugada posible.
        return possibles.ElementAt(0);
    }

    // representación en string del tipo de estrategia.
    public override string ToString()
    {
        return "Jugador Botadobles";
    }
}
