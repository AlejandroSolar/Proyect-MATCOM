namespace Engine;

/// <summary>
/// La clase <c>Move</c> representa una ficha.
/// </summary>
/// <remarks> Es clonable. </remarks>
public class Piece : ICloneable<Piece>
{
    /// <summary>
    /// Crea una nueva ficha con los valores especificados.
    /// </summary>
    /// <param name="left"> parte izquierda de la ficha. </param>
    /// <param name="right"> parte derecha de la ficha. </param>
    /// <param name="value"> valor de la ficha. </param>
    public Piece(int left, int right, int value)
    {
        this.LeftSide = left;
        this.RightSide = right;
        this.PieceValue = value;
    }

    /// <value> <c>LeftSide</c> representa la parte izquierda de la ficha. </value>
    public int LeftSide { get; private set; }

    /// <value> <c>RightSide</c> representa la parte derecha de la ficha. </value>
    public int RightSide { get; private set; }

    /// <value>PieceValue<c> representa el valor de la ficha. </c></value>
    public int PieceValue { get; private set; }

    /// <summary>
    /// Intercambia las partes izquierda y derecha de la ficha.
    /// </summary>
    public void TurnPiece()
    {
        int temp = LeftSide;
        this.LeftSide = this.RightSide;
        this.RightSide = temp;
    }

    /// <summary>
    /// Clona una ficha.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="Piece"/> con los mismos valores de la original.
    /// </returns>
    public Piece Clone()
    {
        return new Piece(this.LeftSide, this.RightSide, this.PieceValue);
    }

    /// <summary>
    /// Representa gráficamente la ficha.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="string"/> con la representacion de la ficha.
    /// </returns>
    public override string ToString()
    {
        return $"[{this.LeftSide}|{this.RightSide}]";
    }

}
