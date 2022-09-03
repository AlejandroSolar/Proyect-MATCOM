namespace Engine;

/// <summary>
/// La clase <c>Round</c> contiene los métodos responsables de la ejecución de un turno 
/// de cada uno de los jugadores que participan en el juego.
/// </summary>
public static class Round
{
    /// <summary>
    /// Ejecuta una ronda con los valores dados por los parametros.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si se ha cumplido alguna de las condiciones de parada.
    /// </returns>
    /// <param name="players"> lista de jugadores que participan en el juego.</param>
    /// <param name="rules"> reglas por las que se guia el juego.</param>
    /// <param name="stopConditions"> condiciones de parada del juego.</param>
    /// <param name="table"> estado actual de la mesa del juego.</param>
    /// <param name="register"> registro actual del juego.</param> 
    /// <param name="couples"> parejas que participan en el juego</param>
    public static bool Run(
        IEnumerable<Player> players,
        IEnumerable<IValid> rules,
        IEnumerable<IStopCondition> stopConditions,
        Board table,
        Register register,
        IEnumerable<Couple> couples
        )
    {
        foreach (var player in players)
        {
            // muestra en pantalla el estado actual del juego
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Es el turno de {player.Name}:");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Tú Mano:");
            player.PrintHand();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Mesa:");
            System.Console.WriteLine(table.ToString());
            Console.WriteLine();

            // devuelve true si alguna condición de parada se cumple
            if (stopConditions.Any(condition => condition.Stop(players, table.Clone(), rules.Clone())))
            {
                return true;
            }

            // alerta al jugador de que se pasó
            if (SkipTurn(player, rules, table))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No puedes jugar");
                Console.ResetColor();
                Console.ReadLine();

                // actualiza el registro con el turno fallido del jugador
                register.Add(new Event(player.Name, null!, Move.Position.left));
                continue;
            }

            while (true)
            {
                // jugada elegida por el jugador al que le corresponde jugar.
                Move move = player.PlayerChoice(table, rules, register, couples);

                // si no es posible la jugada seleccionada por el jugador 
                // no permite jugarla y vuelve a empezar su turno hasta que 
                // seleccione una ficha correcta.
                if (!CheckValid(move, rules, table))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Jugada no valida");
                    Console.ResetColor();
                    Thread.Sleep(800);
                }

                // si la jugada es valida.
                else
                {
                    // juega la ficha en el tablero.
                    Play(player, move, table);

                    // actualiza el registro de juego
                    register.Add(new Event(player.Name, move.Piece, move.PiecePosition));
                    register.PrintActual();
                    break;
                }

            }
            Console.ReadLine();
        }
        return false;
    }

    /// <summary>
    /// Comprueba si el jugador actual tiene posibilidad de jugar,
    /// indicando si debe o no pasar su turno.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que representa si el jugador actual pasa su turno.
    /// </returns>
    /// <param name="player"> jugador al que le corresponde jugar en el turno. </param>
    /// <param name="rules"> reglas de juego actuales. </param>
    /// <param name="table"> mesa de juego actual </param>
    public static bool SkipTurn(Player player, IEnumerable<IValid> rules, Board table)
    {
        // guarda las jugadas posibles del jugador actual.
        IEnumerable<Move> possibles = Auxiliar.PossiblePlays(player.Hand, rules, table);

        // si no tiene jugadas posibles debe pasar su turno.
        if (possibles.Count() == 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ejecuta la jugada seleccionada por el jugador.
    /// </summary>
    /// <param name="player"> jugador actual. </param>
    /// <param name="move"> jugada que se va a ejecutar </param>
    /// <param name="table"> estado actual de la mesa </param>
    private static void Play(Player player, Move move, Board table)
    {
        // si la jugada es por la izquierda de la mesa.
        if (move.PiecePosition == Engine.Move.Position.left)
        {
            // si la ficha debe rotarse la rota y la coloca.
            // si no debe rotarse la coloca directamente en la izquierda de la mesa.
            if (move.IsTurned)
            {
                move.Piece.TurnPiece();
            }
            table.PreAdd(move.Piece);
        }

        // si la jugada es por la derecha de la mesa
        else
        {
            // mismo procedimiento que en la izquierda
            // lo que ahora añade la ficha a la derecha de la mesa.
            if (move.IsTurned)
            {
                move.Piece.TurnPiece();
            }
            table.Add(move.Piece);
        }
        // quita de la mano del jugador la ficha jugada.
        player.Hand.Remove(move.Piece);
    }

    /// <summary>
    /// Verifica si la jugada que se quiere hacer es valida acorde con las reglas de juego.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si la jugada es posible o no.
    /// </returns>
    /// <param name="move"> jugada que se quiere comprobar. </param>
    /// <param name="rules"> reglas por las que se comprueba. </param>
    /// <param nane="table"> estado actual de la mesa </param>
    private static bool CheckValid(Move move, IEnumerable<IValid> rules, Board table)
    {
        // si el valor de la ficha es nula no permite la jugada directamente sin necesidad de comprobar las reglas.
        if (move.Piece is null)
        {
            return false;
        }

        //recorre las reglas y si la jugada es válida para alguna de ellas entonces es posible.
        if (rules.Any(rule => rule.Valid(move, table)))
        {
            return true;
        }

        return false;
    }

}
