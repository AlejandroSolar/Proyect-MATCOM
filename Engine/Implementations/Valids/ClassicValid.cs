namespace Engine;

/// <summary>
/// Reglas clásicas de dominó tradicional.
/// </summary>
public class ClassicValid : IValid
{
    public bool Valid(Move move, Board table)
    {
        // si es la primera jugada se puede jugar siempre.
        if (table.Count == 0)
        {
            return true;
        }

        // si la jugada es por la izquierda de la mesa.
        else if (move.PiecePosition == Move.Position.left)
        {
            // si la ficha no debe girarse.
            if (!move.IsTurned)
            {
                // deben coincidir la parte derecha de la ficha con la parte izquierda
                // de la ficha a la izquierda de la mesa.
                if (move.Piece.RightSide == table.LeftPiece.LeftSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es valida.
                return false;
            }

            // si la ficha debe girarse.
            else
            {
                // deben coincidir la parte izquierda de la ficha con la parte izquierda
                // de la ficha a la izquierda de la mesa.
                if (move.Piece.LeftSide == table.LeftPiece.LeftSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es valida.
                return false;
            }
        }

        // si la jugada es por la derecha de la mesa.
        else
        {
            // si la ficha no debe girarse.
            if (!move.IsTurned)
            {
                // deben coincidir la parte izquierda de la ficha con la parte derecha
                // de la ficha a la derecha de la mesa.
                if (move.Piece.LeftSide == table.RightPiece.RightSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es valida.
                return false;
            }

            // si la ficha debe girarse.
            else
            {
                // deben coincidir la parte derecha de la ficha con la parte derecha
                // de la ficha a la derecha de la mesa.
                if (move.Piece.RightSide == table.RightPiece.RightSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es valida.
                return false;
            }
        }

    }

    // representación en string del tipo de regla.
    public override string ToString() => "Reglas Clasicas";
}