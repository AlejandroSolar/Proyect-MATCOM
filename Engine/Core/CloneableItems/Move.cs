namespace Engine;

/// <summary>
/// La clase <c>Move</c> representa una jugada en el juego.
/// </summary>
/// <remarks> Es clonable. </remarks>
public class Move : ICloneable<Move>
{
    public enum Position
    {
        left,
        right
    }

    /// <value> <c>Piece</c> representa la ficha que se jugó. </value>
    public Piece Piece {get;}

    /// <value> <c>PiecePosition</c> representa la posición de la mesa en la que se jugó. </value>
    public Position PiecePosition {get;}

    /// <value> <c>IsTurned</c> representa si la ficha tuvo que rotarse para poderse colocar. </value>
    public bool IsTurned {get;}

    /// <summary>
    /// Crea una nueva jugada con los valores dados.
    /// </summary>
    /// <param name="piece"> ficha jugada. </param>
    /// <param name="position"> posición de la ficha jugada. </param>
    /// <param name="isTurned"> si esta rotada la ficha jugada. </param>
    public Move (Piece piece, Position position, bool isTurned)
    {
        this.Piece = piece;
        this.PiecePosition = position;
        this.IsTurned = isTurned;
    }

    /// <summary>
    /// Clona una jugada.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="Move"/> con una copia de la ficha e iguales posición y mismo valor de rotación.
    /// </returns>
    public Move Clone() => new Move(this.Piece.Clone(), this.PiecePosition, this.IsTurned);
}