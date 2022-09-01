namespace Engine;

/// <summary>
/// La clase <c>Player</c> representa un jugador.
/// </summary>
/// <remarks> es clonable </remarks>
public class Player : ICloneable<Player>
{
    /// <summary>
    /// Crea un nuevo jugador con los valores especificados.
    /// </summary>
    /// <param name="name"> nombre que va a recibir. </param>
    /// <param name="strategy"> estrategia que va a seguir para jugar. </param>
    public Player(string name, IStrategy strategy)
    {
        this.Name = name;
        this.Hand = new List<Piece>();
        this.Strategy = strategy;
    }

    /// <value> <c>Hand</c> representa la mano del jugador. </value>
    public IList<Piece> Hand { get; private set; }

    /// <value> <c>CurrentPoints</c> representa el valor de la mano del jugador (Suma del valor de todas sus fichas). </value>
    public int CurrentPoints { get { return Points(); } }

    /// <value> <c>Name</c> representa el nombre del jugador. </value>
    public string Name { get; }

    /// <value> <c>Strategy</c> representa la estrategia que sigue el jugador. </value>
    public IStrategy Strategy { get; }

    /// <summary>
    /// Identifica la jugada que el jugador va a realizar
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="Move"/> con la elección de la estrategia.
    /// </returns>
    /// <param name="table"> estado actual de la mesa. </param>
    /// <param name="rules"> reglas por las que se guía el juego actual. </param>
    /// <param name="register"> recuento de los eventos del juego. </param>
    /// <param name="couples"> actuales parejas en juego. </param>
    public Move PlayerChoice(Board table, IEnumerable<IValid> rules, Register register, IEnumerable<Couple> couples)
    {
        // se clona la mano por motivos de seguridad.
        IEnumerable<Piece> cloneHand = this.Hand.Clone(); 

        // almacena la lista de posibles jugadas. 
        IEnumerable<Move> possibles = Auxiliar.PossiblePlays(cloneHand, rules, table);

        // obtiene la jugada elegida por la estrategia actual.
        Move chosedMove = this.Strategy.Choice(cloneHand, possibles, table.Clone(), register.Clone(), couples.Clone());

        // ficha que se va a jugar
        Piece chosedPiece = null!;

        // como choosedMove se eligió a partir de clones de los parametros se comprueba con la mano original.
        chosedPiece = this.Hand.Where
        (
            x => x.LeftSide == chosedMove.Piece.LeftSide
            && x.RightSide == chosedMove.Piece.RightSide
            && x.PieceValue == chosedMove.Piece.PieceValue
        ).First();

        // devuelve la jugada con una instancia de la ficha de la mano original.
        return new Move(chosedPiece, chosedMove.PiecePosition, chosedMove.IsTurned);
    }

    /// <summary>
    /// Representa gráficamente el estado actual de la mano.
    /// </summary>
    public void PrintHand()
    {
        // representa ficha por ficha.
        for (int i = 0; i < Hand?.Count; i++)
        {
            Console.Write($"{Hand[i]}  ");
        }
    }

    /// <summary>
    /// Calcula el valor actual de la mano.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="int"/> con el valor actual de la mano.
    /// </returns>
    private int Points()
    {
        int totalPoints = 0;

        // va sumando el valor de cada ficha.
        for (int i = 0; i < Hand?.Count; i++)
        {
            totalPoints += Hand[i].PieceValue;
        }
        // devuelve la suma total. 
        return totalPoints;
    }

    /// <summary>
    /// Crea un clon del jugador.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="Player"/> con una copia de la mano y de la instancia de la estrategia del original.
    /// </returns>
    public Player Clone()
    {
        Player clone = new Player(this.Name, this.Strategy.Clone());
        clone.Hand = this.Hand.Clone().ToList();
        return clone;
    }

    /// <summary>
    /// Representación gráfica del jugador.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="string"/> con el nombre del jugador y la estrategia que sigue.
    /// </returns>
    public override string ToString()
    {
        return $"{this.Name} ({this.Strategy.ToString()})";
    }
}
