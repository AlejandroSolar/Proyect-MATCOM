namespace Engine;

/// <summary>
/// La clase <c>Game</c> representa un juego de dominó personalizable
/// y contiene todos los métodos necesarios para la ejecución del mismo.
/// </summary>
public class Game
{
    /// <summary>
    /// crea una instancia de <typeparamref name="Game"/> mediante los paramentros dados.
    /// </summary>
    /// <param name="players"> listado con los jugadores que van a participar. </param>
    /// <param name="maxDouble"> doble máximo que admiten las fichas de dominó. </param>
    /// <param name="handCapacity"> cantidad de fichas inicialmente en cada mano.  </param>
    /// <param name="rules"> reglas que se van a aplicar durante el juego. </param>
    /// <param name="dealer"> repartidor de fichas del juego. </param>
    /// <param name="orderers"> ordenadores de turnos del juego. </param>
    /// <param name="swiftTurns"> cantidad de turnos que deben pasar para cambiar de ordenador (Si es 0 no se cambia). </param>
    /// <param name="evaluators"> evaluadores de las fichas. </param>
    /// <param name="stopConditions"> condiciones de parada del juego. </param>
    /// <param name="winConditions"> condiciones de victoria del juego. </param>
    /// <param name="couples"> parejas que van a participar (si esta vacío entonces el juego no es por parejas). </param>
    public Game(

        IList<Player> players,
        int maxDouble,
        int handCapacity,
        IEnumerable<IValid> rules,
        IDealer dealer,
        IEnumerable<ITurnOrderer> orderers,
        int swiftTurns,
        IEnumerable<IEvaluator> evaluators,
        IEnumerable<IStopCondition> stopConditions,
        IEnumerable<IWinCondition> winConditions,
        IEnumerable<Couple> couples

    )

    {
        this.Players = players;
        this.Couples = couples;
        this.WinConditions = winConditions;
        this.StopConditions = stopConditions;
        this.Table = new Board();
        this.Box = new List<Piece>();
        this.Rules = rules;
        this.Orderers = orderers;
        this.SwiftTurns = swiftTurns;
        this.Register = new Register();

        // crea las fichas y las almacena en una caja para ser posteriormente repartidas. 
        FillBox(maxDouble, evaluators);

        // limpia las manos de los jugadores porque en caso de torneos mantienen fichas de juegos anteriores.
        foreach (var item in this.Players)
        {
            item.Hand.Clear();
        }

        // reparte las fichas a cada jugador.
        dealer.Deal(players, Box.Clone().ToList(), handCapacity);

        // organiza el orden de los turnos de los jugadores.
        Orderers.First().Organize(Players, Couples);
    }

    /// <value> <c>Register</c> representa el registro del juego actual.</value>
    public Register Register { get; private set; }

    /// <value> <c>StopConditions</c> representa las condiciones de parada del juego.</value>
    private IEnumerable<IStopCondition> StopConditions { get; }

    /// <value> <c>WinConditions</c> representa las condiciones de victoria del juego.</value>
    private IEnumerable<IWinCondition> WinConditions { get; }

    /// <value> <c>Orderers</c> representa los ordenadores de turno del juego.</value>
    private IEnumerable<ITurnOrderer> Orderers { get; }

    /// <value> <c>Rules</c> representa las reglas del juego.</value>
    private IEnumerable<IValid> Rules { get; }

    /// <value> <c>Players</c> representa los jugadores que participan en el juego.</value>
    private IList<Player> Players { get; set; }

    /// <value> <c>Winners</c> es un listado con los ganadores del juego.</value>
    public IList<string>? Winners { get; private set; }

    /// <value> <c>Couples</c> representa las parejas en juego.</value>
    private IEnumerable<Couple> Couples { get; }

    /// <value> <c>Table</c> representa el estado actual de la mesa del juego.</value>
    private Board Table { get; set; }

    /// <value> <c>Box</c> lugar de almacenamiento de las fichas</value>
    private IList<Piece> Box { get; set; }

    /// <value> <c>SwiftTurns</c> cantidad de turnos para el cambio de ordenador.</value>
    private int SwiftTurns { get; }


    /// <summary>
    /// Crea las fichas de domino de acuerdo al doble máximo y a los evaluadores y las añade a la caja.
    /// </summary>
    /// <param name="evaluators"> evaluadores de fichas en el juego. </param>
    /// <param name="maxDouble"> doble máximo que admiten las fichas. </param>
    private void FillBox(int maxDouble, IEnumerable<IEvaluator> evaluators)
    {
        // recorre valor a valor hasta el doble máximo sin repetir fichas.
        for (int i = 0; i <= maxDouble; i++)
        {
            for (int j = i; j <= maxDouble; j++)
            {
                // el valor de la ficha se convierte en la suma de los valores dados por cada evaluador.
                int pieceValue = evaluators.Sum(x => x.Evaluate(i, j));

                // añade la ficha a la caja.
                Box.Add(new Piece(i, j, pieceValue));
            }
        }
    }

    /// <summary>
    /// Ejecuta el juego de principio a fin de acuerdo con los parametros dados,
    /// premia a los ganadores e imprime en pantalla el resultado final.
    /// </summary>
    public void RunGame()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("INICIANDO PARTIDA...");
        Console.ResetColor();
        Thread.Sleep(2000);

        // cantidad de rondas que han pasado hasta el momento.
        int rounds = 0;

        // ordenador actual.
        int currentOrganizer = 1;

        while (true)
        {
            rounds += 1;

            // si alguna de las condiciones de parada detiene la ronda, automaticamente detiene el juego.
            if (Round.Run(Players, Rules, StopConditions, Table, Register, Couples))
            {
                break;
            }

            // si el número de rondas alcanza el escogido por el usuario, cambia de ordenador 
            // y vuelve a ordemar los turnos de los jugadores.
            if (rounds == SwiftTurns && SwiftTurns != 0)
            {
                Swift(ref currentOrganizer);
                rounds = 0;
            }
        }

        // cuando alguna de las condiciones de parada es aplicada recorre las condiciones de victoria
        // y actualiza la lista de ganadores en concecuencia.
        foreach (var condition in WinConditions)
        {
            this.Winners = condition.Winners(this.Players.Clone()).ToList();

            // si una de las condiciones de victoria se aplica el recorrido finaliza,
            // otorgando la victoria solo a los jugadores beneficiados por esa condicion.
            if (this.Winners.Count > 0)
            {
                break;
            }
        }

        // si hay división en parejas otorga la victoria a la pareja
        if (Couples.Count() > 0)
        {
            CoupleWin();
        }

        // finaliza el juego
        GameOver();
    }

    /// <summary>
    /// Representa en pantalla el estado final del juego,
    /// muestra los ganadores si los hubo, el estado de las manos de todos
    /// jugadores y el estado final de la mesa.
    /// </summary>
    private void GameOver()
    {
        Console.Clear();
        if (Winners!.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No hubo ganadores en esta partida");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------");
            Console.ResetColor();
        }

        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ganador(es):");
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var winner in Winners)
            {
                Console.WriteLine(winner);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------");
            Console.ResetColor();
        }
        // ordena los jugadores de menor a mayor por su puntuacion final.
        Players = Players.OrderBy(player => player.CurrentPoints).ToList();

        Console.WriteLine();
        Console.WriteLine();

        // recorre los jugadores e imprime sus respectivas puntuaciones.
        foreach (var player in Players)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{player.ToString()}");
            Console.ResetColor();
            Console.WriteLine($": {player.CurrentPoints} Puntos ");
            player.PrintHand();
            Console.WriteLine();
            Console.WriteLine();
        }

        // imprime el estado final de la mesa.
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Estado Final de la Mesa:");
        Console.ResetColor();
        System.Console.WriteLine(Table);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Pulse Enter");
        Console.ReadLine();

        Register.PrintRegister();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Fin de la partida.\n");
        Console.ResetColor();

        Console.WriteLine("Pulse Enter");
        Console.ReadLine();

    }

    /// <summary>
    /// Actualiza la lista de ganadores teniendo en cuenta las parejas,
    /// si uno de los miembros de la pareja ganó pero el otro no, otorga
    /// tambien la victoria al otro.
    /// </summary>
    private void CoupleWin()
    {
        // recorre la lista de ganadores
        for (int i =0 ; i < Winners!.Count; i++)
        {
            // recorre las parejas
            foreach (var couple in Couples)
            {
                // si no esta la pareja de algún ganador la añade a la lista de ganadores.
                if (Winners[i] == couple.Player1.Name && !Winners.Contains(couple.Player2.Name))
                {
                    Winners.Add(couple.Player2.Name);
                }

                else if (Winners[i] == couple.Player2.Name && !Winners.Contains(couple.Player1.Name))
                {
                    Winners.Add(couple.Player1.Name);
                }
            }
        }
    }

    /// <summary>
    /// aplica el criterio de ordenación actual y
    /// cambia al ordenador siguiente modificando la variable <typeparamref name="int"/> currentOrganizer.
    /// </summary>
    private void Swift(ref int currentOrganizer)
    {
        // si solo hay un ordenador de turnos el siguiente ordenador es el mismo.
        if (Orderers.Count() == 1)
        {
            currentOrganizer = 0;
        }

        ITurnOrderer current = Orderers.ElementAt(currentOrganizer);

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Aplicando {current.ToString()}...");
        Console.ResetColor();
        Thread.Sleep(2000);

        // ordena la lista de jugadores de acuerdo con el criterio de ordenación actual.
        current.Organize(Players, Couples);

        // pasa al siguiente ordenador.
        currentOrganizer += 1;

        // si se pasa de cantidad de ordenadores regresa al primero.
        if (currentOrganizer == Orderers.Count())
        {
            currentOrganizer = 0;
        }
    }
}