namespace Engine;

/// <summary>
/// La clase <c>Tournament</c> encierra la responsabilidad de ejecución
/// de un torneo, que no es más que una serie de juegos consecutivos.
/// </summary>
public class Tournament
{
    /// <summary>
    /// Crea un nuevo torneo con los valores indicados por los parámetros.
    /// </summary>
    /// <param name="score"> puntuación que se debe lograr para ganar el torneo. </param>
    /// <param name="games"> cantidad de juegos que deben jugarse. </param>
    /// <param name="prize"> forma de premiar la victoria de un juego en el torneo. </param>
    /// <param name="players"> listado de jugadores que participan en el torneo. </param>
    public Tournament(int score, int games, IScore prize, IList<Player> players)
    {
        this.Score = score;
        this.Players = players;
        this.Games = games;
        this.Prize = prize;

        // inicia la tabla de posiciones vacía. 
        this.Scoreboard = new Dictionary<string, int>();

        // llena la tabla de posiciones con los nombres de los jugadores.
        foreach (var player in players)
        {
            Scoreboard.Add(player.Name, 0);
        }
    }
    /// <value> <c>Score</c> representa el puntaje necesario para otorgar la victoria en el torneo.</value>
    private int Score { get; }

    /// <value> <c>Games</c> representa la cantidad de juegos que deben jugarse para finalizar el torneo.</value>
    private int Games { get; }

    /// <value> <c>Prize</c> representa la forma que tiene el torneo de premiar la victoria de un juego.</value>
    private IScore Prize { get; }

    /// <value> <c>ScoreBoard</c> constituye la tabla de posiciones del torneo.</value>
    public Dictionary<string, int> Scoreboard { get; private set; }

    /// <value> <c>Players</c> representa el listado de jugadores que participan en el torneo.</value>
    private IList<Player> Players { get; }

    /// <summary>
    /// Ejecuta el torneo.
    /// </summary>
    public void Run()
    {
        // crea tantos juegos como el numero seleccionado al crear el torneo.
        for (int i = 0; i < Games; i++)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Partida #{i + 1}");
            Console.ResetColor();
            Thread.Sleep(2000);

            // crea el juego acorde a las elecciones del usuario.
            Game currentGame = GameCreator.Game(Players);

            // ejecuta el juego.
            currentGame.RunGame();

            // actualiza la lista de posiciones otorgando puntos de acuerdo con el criterio seleccionado de Prize
            Prize.GrantPrize(Players.Clone(), new List<string>(currentGame.Winners!), Scoreboard);

            // muestra en pantalla la tabla de posiciones.
            PrintScoreboard();

            // si se cumplieron los requisitos de victoria, finaliza el torneo.
            if (Win(Scoreboard, Score))
            {
                break;
            }
        }
        // muestra en pantalla los ganadores, organiza y muestra el estado final de la tabla de posiciones.
        TournamentOver();
    }

    /// <summary>
    /// Verifica si algún jugador logró la cantidad de puntos necesarios.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si algún jugador gana el torneo.
    /// </returns>
    /// <param name="scoreBoard"> estado actual de la tabla de posiciones. </param>
    /// <param name="points"> cantidad de puntos necesarios para finalizar el torneo. </param>
    private bool Win(Dictionary<string, int> scoreBoard, int points)
    {
        // si algún jugador alcanza la cantidad de puntos necesarios devuelve verdadero.
        if (scoreBoard.Any(x => x.Value >= points))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Muestra en pantalla las estadísticas finales del torneo.
    /// </summary>
    private void TournamentOver()
    {
        //organiza la tabla de posiciones de mayor a menor por los puntos alcanzados.
        var finalScoreboard = Scoreboard.OrderByDescending(x => x.Value);

        // nos quedamos con los jugadores con la mayor cantidad de puntos
        IEnumerable<string> winners = from x in finalScoreboard where (x.Value == finalScoreboard.First().Value) select x.Key;

        // muestra en pantalla el listado de los ganadores.
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("TORNEO TERMINADO");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Ganadores:");
        Console.WriteLine("----------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();

        foreach (var item in winners)
        {
            Console.WriteLine(item);
        }

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Enter para ver la tabla de posiciones");
        Console.ReadLine();

        // imprime la tabla de posiciones. 
        PrintScoreboard();
    }

    /// <summary>
    /// Muestra en pantalla el estado final de la tabla de posiciones.
    /// </summary>
    private void PrintScoreboard()
    {
        // crea una tabla de posiciones ordenada.
        var temp = Scoreboard.OrderByDescending(X => X.Value);

        // muestra la tabla en pantalla
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Tabla de Posiciones:");
        Console.WriteLine("--------------------");
        Console.WriteLine();
        Console.ResetColor();

        foreach (var item in temp)
        {
            Console.Write($"{item.Key}:  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{item.Value}");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Enter para continuar");
        Console.ReadLine();
    }
}
