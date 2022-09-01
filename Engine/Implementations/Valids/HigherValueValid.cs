namespace Engine;

/// <summary>
/// Declara como válida la jugada si el valor de la ficha que se desea jugar es mayor
/// que el valor de la ficha de la posición donde se quiere jugar.
/// </summary>
public class HigherValueValid : IValid
{
    public bool Valid(Move move, Board table)
    {
        // si la jugada es la primera es válida.
        if (table.Count == 0)
        {
            return true;
        }
        
        int auxiliar = 0;

        // si la jugada es por la izquierda de la mesa
        if (move.PiecePosition == Move.Position.left)
        {
            // guarda el valor de la ficha a la izquierda de la mesa.
            auxiliar = table.LeftPiece.PieceValue;
        }

        // en caso contrario guarda el valor de la ficha a la derecha de la mesa.
        else
        {
            auxiliar = table.RightPiece.PieceValue;
        }


        // si el valor de la ficha que se desa jugar es mayor o igual
        // al valor de auxiliar entonces la jugada será válida
        if (auxiliar <= move.Piece.PieceValue)
        {
            return true;
        }

        return false;
    }

    // representación en string del tipo de regla.
    public override string ToString()
    {
        return "Regla de mayor valor";
    }
}