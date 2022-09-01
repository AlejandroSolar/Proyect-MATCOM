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
        for (int i = 0; i < hand.Count(); i++)
        {
            // indica si hay alguna jugada posible con la ficha actual de la mano.
            bool contains = false;

            // recorre la lista de posibles.
            for (int j = 0; j < possibles.Count(); j++)
            {
                // Si hay una jugada posible con esta ficha.
                if (hand.ElementAt(i) == possibles.ElementAt(j).Piece)
                {
                    // se actualiza el bool y se sale de este ciclo.
                    contains = true;
                    break;
                }
            }

            // si no hay ninguna jugada posible con esta ficha
            if (!contains)
            {
                // añado una jugada con esta ficha (que evidentemente será incorrecta) a newPossibles
                newPossibles.Add(new Move(hand.ElementAt(i), Move.Position.left, false));
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
    public override string ToString()
    {
        return "Jugador Borracho";
    }
}
