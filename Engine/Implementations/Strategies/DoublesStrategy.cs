namespace Engine;

/// <summary>
/// Estrategia que prioriza jugar las fichas que son dobles.
/// </summary>
public class DoublesStrategy : IStrategy
{
    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // recorre la lista de jugadas posibles
        for (int i = 0; i < possibles.Count(); i++)
        {
            // si la ficha en possibles en el indice i es doble devuelve esa jugada.
            if (possibles.ElementAt(i).Piece.LeftSide == possibles.ElementAt(i).Piece.RightSide)
            {
                return possibles.ElementAt(i);
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
