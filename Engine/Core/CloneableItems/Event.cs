namespace Engine;

/// <summary>
/// La clase <c>Event</c> representa cada jugada que guarda el registro del juego.
/// </summary>
/// <remarks> Es clonable. </remarks>
public class Event : ICloneable<Event>
{
    /// <value> <c>PlayerName</c> representa el nombre del jugador que hizo el movimiento. </value>
    public string PlayerName { get; private set; }

    /// <value> <c>PlayedPiece</c> representa la ficha jugada en el turno. </value>
    public Piece PlayedPiece { get; private set; }

    /// <value> <c>Position</c> representa la posición por la que se jugó la ficha. </value>
    public Move.Position Position { get; private set; }

    /// <summary>
    /// Crea un nuevo evento con los valores especificados por los parametros.
    /// </summary>
    /// <param name="name"> nombre del jugador. </param>
    /// <param name="piece"> ficha jugada. </param>
    /// <param name="position"> posición por la que se jugó. </param>
    public Event(string name, Piece piece, Move.Position position)
    {
        this.PlayerName = name;
        this.PlayedPiece = piece;
        this.Position = position;
    }

    /// <summary>
    /// Representa el evento gráficamente.
    /// </summary>
    /// <param name="first"> indica si es o nó es el primer evento del juego. </param>
        public void PrintEvent(bool first)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(PlayerName);
        Console.ResetColor();

        if (PlayedPiece is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Se pasó");
            Console.ResetColor();
            return;
        }

        Console.Write(" Jugó la ficha ");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(PlayedPiece.ToString());
        Console.ResetColor();
        if (first)
        {
            Console.WriteLine();
        }

        // si no es el primer turno indica en que posición se jugó
        if (!first)
        {
            string pos = "";
            switch (Position)
            {
                case Move.Position.left: pos = "izquierda"; break;
                case Move.Position.right: pos = "derecha"; break;
            }
            Console.Write(" a la ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(pos);
            Console.ResetColor();
        }

    }

    /// <summary>
    /// Clona un evento.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="Event"/> con el mismo nombre del jugador,
    /// una copia de la ficha jugada y la misma posición del original.
    /// </returns>
    public Event Clone()
    {
        // en caso de pasarse el jugador la ficha es nula
        if (this.PlayedPiece is null)
        {
            return new Event(this.PlayerName, null!, this.Position);
        }
        return new Event(this.PlayerName, this.PlayedPiece.Clone(), this.Position);
    }
}
