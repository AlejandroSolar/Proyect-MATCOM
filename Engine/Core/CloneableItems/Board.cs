using System.Text;

namespace Engine;

/// <summary>
/// La clase <c>Board</c> representa el tablero de juego.
/// </summary>
/// <remarks> es clonable </remarks>
public class Board : ICloneable<Board>
{
    /// <value> <c>Table</c> representa el estado actual de la mesa. </value>
    private IList<Piece> Table { get; set; }

    /// <value> <c>Value</c> representa el valor total y actual en la mesa (suma del valor de las fichas jugadas). </value>
    public int Value { get => Table.Sum(x => x.PieceValue); }

    /// <value> <c>Count</c> representa la cantidad de fichas colocadas actualmente en la mesa. </value>
    public int Count { get => Table.Count; }

    /// <value> <c>LeftPiece</c> accede directamente a la ficha colocada a la izquierda de la mesa. </value>
    public Piece LeftPiece { get => Table.First(); }

    /// <value> <c>RightPiece</c> accede directamente a la ficha colocada a la derecha de la mesa. </value>
    public Piece RightPiece { get => Table.Last(); }

    /// <summary>
    /// Este constructor inicia una nueva mesa vacía
    /// </summary>
    public Board() => this.Table = new List<Piece>();

    /// <summary>
    /// Coloca una ficha a la derecha de la mesa.
    /// </summary>
    /// <param name="piece"> pieza que se debe colocar </param>
    public void Add(Piece piece) => Table.Add(piece);

    /// <summary>
    /// Coloca una ficha a la izquierda de la mesa.
    /// </summary>
    /// <param name="piece"> pieza que se debe colocar </param>
    public void PreAdd(Piece piece) => Table.Insert(0, piece);

    /// <summary>
    /// Crea un clon del estado actual de la mesa.
    /// </summary>
    /// <returns>
    /// Una nueva mesa con copias de cada una de las fichas colocadas en la mesa original.
    /// </returns>
    public Board Clone() => new Board() { Table = this.Table.Clone().ToList() };

    /// <summary>
    /// Representa gráficamente de la mesa.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="string"/> con la representacion de la mesa impresa de izquierda a derecha.
    /// </returns>
    public override string ToString()
    {
        StringBuilder temp = new StringBuilder();

        // va añadiendo cada ficha y al finalizar devuelve el string
        foreach (var item in Table)
        {
            temp.Append($"-{item.ToString()}-");
        }
        return temp.ToString();
    }
}
