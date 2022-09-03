namespace Engine;

/// <summary>
/// Declara como válida la jugada si el valor de la parte de la ficha que se desea jugar
/// es mayor que la parte de la ficha con la que coincide en la mesa.
/// </summary>
public class HigherExtremeValid : IValid
{
    public bool Valid(Move move, Board table)
    {
        // si la jugada es la primera es válida.
        if (table.Count == 0)
        {
            return true;
        }
 
        //ficha auxiliar
        Piece auxiliarPiece;

        // si la jugada que se desea jugar es por la izquierda de la mesa.
        if (move.PiecePosition == Move.Position.left)
        {
            //guarda la ficha de la izquierda de la mesa.
            auxiliarPiece = table.LeftPiece;

            // si la ficha que se desea jugar no debe girarse.
            if (!move.IsTurned)
            {
                // la jugada será valida si la parte derecha de la ficha que se desea jugar
                // es mayor que la parte izquierda de la ficha coincidente con ella.
                if (move.Piece.RightSide > auxiliarPiece.LeftSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es válida
                else return false;
            }

            // si la ficha que se desea jugar debe girarse
            // la jugada será valida si la parte izquierda de la ficha que se desea jugar
            // es mayor que la parte izquierda de la ficha coincidente con ella.
            if (move.Piece.LeftSide > auxiliarPiece.LeftSide)
            {
                return true;
            }

            // en caso contrario la jugada no es válida
            else return false;


        }

        // si la ficha que se desea jugar es por la derecha de la mesa.
        else
        {
            //guarda la ficha de la derecha de la mesa.
            auxiliarPiece = table.RightPiece;

            // si la ficha que se desea jugar no debe girarse.
            if (!move.IsTurned)
            {
                // la jugada será valida si la parte izquierda de la ficha que se desea jugar
                // es mayor que la parte derecha de la ficha coincidente con ella.
                if (move.Piece.LeftSide > auxiliarPiece.RightSide)
                {
                    return true;
                }

                // en caso contrario la jugada no es válida
                else return false;
            }

            // si la ficha que se desea jugar debe girarse
            // la jugada será valida si la parte derecha de la ficha que se desea jugar
            // es mayor que la parte derecha de la ficha coincidente con ella.
            if (move.Piece.RightSide > auxiliarPiece.RightSide)
            {
                return true;
            }

            // en caso contrario la jugada no es válida
            else return false;
        }

    }

    // representación en string del tipo de regla.
    public override string ToString() => "Regla del mayor extremo";
}