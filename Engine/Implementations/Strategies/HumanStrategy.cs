namespace Engine;

/// <summary>
/// Jugador Humano. pregunta al usuario por la ficha a jugar.
/// </summary>
public class HumanStrategy : IStrategy
{
    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // guarda la ficha elegida por el usuario.
        Piece selectedPiece = Auxiliar<Piece>.Selector($"Estado de la mesa:\n{table.ToString()}\n\nElija su jugada", hand);

        // si es la primera jugada del juego la coloca directamente.
        if (table.Count == 0)
        {
            return new Move(selectedPiece, Move.Position.left, false);
        }

        // guarda la jugada que se va a ejecutar.
        Move selectedPlay;

        // crea una nueva lista de jugadas posibles, que contendrá todas las jugadas posibles con la ficha seleccionada.
        Move[] newPossibles = possibles.Where(x => x.Piece.Equals(selectedPiece)).ToArray();

        // si no hay ninguna jugada con la ficha elegida.
        if (newPossibles.Length == 0)
        {
            // guarda en selectedPlay una nueva jugada con esta ficha, que será marcada como incorrecta en la ejecución del juego.
            selectedPlay = new Move(selectedPiece, Move.Position.left, false);
        }

        // si solo hay una jugada posible.
        else if (newPossibles.Length == 1)
        {
            // guarda en selectedPlay esa única jugada.
            selectedPlay = newPossibles.First();
        }

        // si hay más de una jugada posible.
        else
        {
            // si la jugada es ambigua, es decir, que puede jugarse tanto por izquierda como por derecha.
            if (AmbiguousPlay(newPossibles))
            {
                // Se pregunta al usuario por la posición.
                Move.Position position = AskForPosition();

                newPossibles = newPossibles.Where(x => x.PiecePosition == position).ToArray();

                // si solo queda una jugada posible
                if (newPossibles.Length == 1)
                {
                    // guarda en selectedPlay esa jugada.
                    selectedPlay = newPossibles.First();
                }

                // si queda más de una jugada es porque la ficha
                // se puede jugar por esa posición girandose y sin girar.
                else
                {
                    // guarda en selectedPlay la jugada con la elección del usuario de si desea o no girarla.
                    selectedPlay = new Move(selectedPiece, position, AskForTurn(selectedPiece));
                }
            }

            // si no es ambigua significa que la ficha se puede jugar por una sola posición pero es válida
            // tanto girada como sin girar.
            else
            {
                // guarda en selectedPlay la jugada en la unica posición posible con la elección del usuario de si girarla o no.
                selectedPlay = new Move(selectedPiece, newPossibles.First().PiecePosition, AskForTurn(selectedPiece));
            }
        }

        return selectedPlay;
    }

    /// <summary>
    /// Comprueba si una de las fichas puede ser jugada tanto por derecha como por izquierda.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si la ficha es ambigua.
    /// </returns>
    /// <param name="newPossibles"> Colección con todas las jugadas posibles con una ficha en específico </param>
    private bool AmbiguousPlay(IEnumerable<Move> newPossibles)
    {
        // representan si la ficha es jugable por izquierda y por derecha.
        bool left = newPossibles.Any( x => x.PiecePosition == Move.Position.right);
        bool right = newPossibles.Any( x => x.PiecePosition == Move.Position.left);

        return left && right;
    }

    /// <summary>
    /// Pregunta al ususario por la posición por la que desea jugar una ficha.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="Position"/> que indica la posición elegida por el usuario.
    /// </returns>
    private Move.Position AskForPosition()
    {
        // pregunta al usuario.
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("La Ficha que desea jugar se puede colocar en mas de una posición");
        Console.ResetColor();
        Console.WriteLine("Elija en cual desea jugarla:");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[I] ");
        Console.ResetColor();
        Console.WriteLine("Izquierda");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[D] ");
        Console.ResetColor();
        Console.WriteLine("Derecha");

        // retorna izquierda o derecha en dependencia de la elección del usuario.
        while (true)
        {
            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.I:
                    return Move.Position.left;

                case ConsoleKey.D:
                    return Move.Position.right;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Pregunta al usuario si desea girar la ficha.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si se desea girar la ficha.
    /// </returns>
    /// <param name="piece"> ficha por la que preguntar </param>
    private bool AskForTurn(Piece piece)
    {
        // si la ficha es un doble no se debe preguntar.
        if (piece.RightSide == piece.LeftSide)
        {
            return false;
        }

        // pregunta al usuario.
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("¿Desea girar la ficha antes de colocarla?");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[S] ");
        Console.ResetColor();
        Console.WriteLine("Si");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[N] ");
        Console.ResetColor();
        Console.WriteLine("No");

        // retorna verdadero o falso en dependencia de la elección del usuario.
        while (true)
        {
            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.N:
                    return false;

                case ConsoleKey.S:
                    return true;

                default:
                    break;
            }
        }
    }

    // representación en string del tipo de estrategia.
    public override string ToString() => "Jugador Humano";
}