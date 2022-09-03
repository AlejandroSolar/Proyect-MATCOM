namespace Engine;

/// <summary>
/// Estrategia que añade jugadas no validas a la lista de posibles y las trata de jugar.
/// </summary>
/// <remarks>
/// Hereda de <typeparamref name="RamdomStrategy"/>.
/// </remarks>
public class DrunkStrategy : RandomStrategy
{
    public override Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // crea una lista de posibles con las mismas jugada que la recibida como parámetro
        IList<Move> newPossibles = possibles.ToList();

        // cantidad de fichas que a añadido.
        int addPiece = 0;

        // recorre la mano del jugador.
        foreach (var piece in hand)
        {
            // si no hay ninguna jugada posible con esta ficha
            if (!possibles.Any(x => x.Piece.Equals(piece)))
            {
                // añado una jugada con esta ficha (que evidentemente será incorrecta) a newPossibles
                newPossibles.Add(new Move(piece, Move.Position.left, false));
                addPiece++;
            }

            // si ya se añadieron 3 jugadas no validas detiene el ciclo.
            if (addPiece == 3)
            {
                break;
            }
        }

        // devuelve una jugada aleatoria entre la nueva lista de posibles, pudiendo elegir una de las no válidas que añadió.
        return base.Choice(hand, newPossibles, table, register, couples);
    }

    // representación en string del tipo de estrategia.
    public override string ToString() => "Jugador Borracho";
}
