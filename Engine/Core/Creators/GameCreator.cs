namespace Engine;

/// <summary>
/// La clase <c>GameCreator</c> se encarga de realizar todos los procesos necesarios para la creación de un juego.
/// </summary>
public static class GameCreator
{
    /// <value> <c>Evaluators</c> contiene el tipo de cada uno de los evaluadores de fichas implementados.</value>
    private static IEnumerable<Type> Evaluators = Reflection<IEvaluator>.TypesCollectionCreator();

    /// <value> <c>Rules</c> contiene el tipo de cada una de las reglas implementadas.</value>  
    private static IEnumerable<Type> Rules = Reflection<IValid>.TypesCollectionCreator();

    /// <value> <c>Orderers</c> contiene el tipo de cada uno de los ordenadores de turno implementados.</value>
    private static IEnumerable<Type> Orderers = Reflection<ITurnOrderer>.TypesCollectionCreator();

    /// <value> <c>Dealers</c> contiene el tipo de cada uno de los repartidores de fichas implementados.</value>
    private static IEnumerable<Type> Dealers = Reflection<IDealer>.TypesCollectionCreator();

    /// <value> <c>StopConditions</c> contiene el tipo de cada una de las condiciones de parada implementadas.</value>
    private static IEnumerable<Type> StopConditions = Reflection<IStopCondition>.TypesCollectionCreator();

    /// <value> <c>WinConditions</c> contiene el tipo de cada una de las condiciones de victoria implementadas.</value>
    private static IEnumerable<Type> WinConditions = Reflection<IWinCondition>.TypesCollectionCreator();

    /// <value> <c>DefaultOptions</c> contiene el tipo de cada uno de los juegos por defecto implementados.</value>
    private static IEnumerable<Type> DefaultOptions = Reflection<IDefaultGames>.TypesCollectionCreator();


    /// <summary>
    /// Crea un juego y establece todas las configuraciónes necesarias sobre los aspectos personalizables
    /// de este mediante las elecciones del usuario.
    /// </summary>
    /// <returns>
    /// Una instancia de <typeparamref name="Game"/> con la lista de jugadores recibida y las instrucciones del usuario.
    /// </returns>
    /// <param name="players"> lista de jugadores que participarán en el juego </param>
    public static Game Game(IList<Player> players)
    {
        // máximo doble que se va a permitir en el juego
        int maxDouble = 0;

        // cantidad de fichas que va a tener cada mano
        int handCapacity = 0;

        // cantidad de turnos que han de pasar para cambiar el orden los turnos de los jugadores 
        //(Si es 0 no se cambia el orden en todo el juego).
        int swiftTurns = 0;
        
        //Repartidor del juego que se elegirá más adelante.
        IDealer dealer = null!;

        // crea colecciónes vacias de los aspectos que serán elegidos por el usuario.
        IList<IEvaluator> evaluators = new List<IEvaluator>();
        IList<IValid> rules = new List<IValid>();
        IList<ITurnOrderer> orderers = new List<ITurnOrderer>();
        IList<IStopCondition> stopConditions = new List<IStopCondition>();
        IList<IWinCondition> winConditions = new List<IWinCondition>();
        IEnumerable<Couple> couples = new List<Couple>();

        // si el usuario escoge jugar por parejas se crea la lista de parejas.
        if ((players.Count() % 2 == 0) && (Auxiliar.YesOrNo("¿Desea jugar por parejas?")))
        {
            couples = CreateCouples(players);
        }

        // almacena instancias de cada uno de los juegos por defecto a travez de la colección de tipos de juegos por defecto.
        IList<IDefaultGames> defaultOptions = Reflection<IDefaultGames>.CollectionCreator(DefaultOptions).ToList();

        // verifica que los juegos por defecto implementados sean válidos.
        var validDefaultGames = from x in defaultOptions where DoublesAndMax(x.MaxDouble, x.HandCapacity, players.Count()) select x;
        
        if (validDefaultGames.Count() > 0 && Auxiliar.YesOrNo("¿Desea utilizar alguna configuración por defecto?"))
        {
            // si el usuario accedió entonces elige entre los juegos por defecto implementados y actualiza los campos.
            IDefaultGames defaultGame = Auxiliar<IDefaultGames>.Selector("Seleccione un juego predeterminado.", validDefaultGames);

            maxDouble = defaultGame.MaxDouble;
            handCapacity = defaultGame.HandCapacity;
            swiftTurns = defaultGame.SwiftTurns;
            dealer = defaultGame.Dealer;
            evaluators = defaultGame.Evaluators.ToList();
            rules = defaultGame.Rules.ToList();
            stopConditions = defaultGame.StopConditions.ToList();
            winConditions = defaultGame.WinConditions.ToList();
            orderers = defaultGame.Organizers.ToList();
        }

        else
        {
            // Pregunta al usuario por los valores de maxDouble y handCapacity y se asegura que se elijan valores compatibles.
            // es decir, que se puedan repartir las fichas correctamente entre todos los jugadores.
            while (true)
            {
                maxDouble = Auxiliar.NumberSelector("Elija el doble máximo en las fichas", 0);
                handCapacity = Auxiliar.NumberSelector("Elija la cantidad de fichas en la mano de cada jugador", 1);

                if (!DoublesAndMax(maxDouble, handCapacity, players.Count()))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Con esta configuración no se pueden repartir las fichas correctamente");
                    Console.WriteLine("Asegúrese que es posible repartir el número de Fichas a cada jugador");
                    Console.WriteLine();
                    Console.WriteLine("Enter para reelegir configuración");
                    Console.ReadLine();
                    continue;
                }

                break;
            }

            // va preguntando aspecto por aspecto al usuario para que este configure el juego en su totalidad.
            Auxiliar<IStopCondition>.ConfigSelector(stopConditions, StopConditions, "Elija una condición de parada para la partida");
            Auxiliar<IWinCondition>.ConfigSelector(winConditions, WinConditions, "Elija una condición de Victoria para la partida");
            Auxiliar<IValid>.ConfigSelector(rules, Rules, "Elija un Validador de jugadas para la partida");
            Auxiliar<IEvaluator>.ConfigSelector(evaluators, Evaluators, "Elija un evaluador de fichas para la partida");
            Auxiliar<ITurnOrderer>.ConfigSelector(orderers, Orderers, "Elija un ordenador de turnos para la partida");

            // si hay más de un ordenador de turnos el usuario elige cada cuantas rondas se debe cambiar entre ellos.
            if (orderers.Count() > 1)
            {
                swiftTurns = Auxiliar.NumberSelector("Ha elegido mas de un ordenaror de turnos \nElija cada cuantas rondas estos cambiaran el orden de los jugadores", 1);
            }

            // si hay un solo ordenador de turnos el usuario decide si desea reordenar cada cierta cantidad de turnos.
            else if (orderers.Count() == 1 && Auxiliar.YesOrNo("¿Desea reordenar el juego tras una cierta cantidad de turnos?"))
            {
                swiftTurns = Auxiliar.NumberSelector("¿Cada Cuantos turnos se reordenaran los jugadores?", 1);
            }

            // el usuario selecciona el repartidor de fichas del juego.
            dealer = Auxiliar<IDealer>.Selector("Elija el repartidor de fichas de la partida", Reflection<IDealer>.CollectionCreator(Dealers));
        }

        // devuelve una instancia del juego con las configuraciones dadas.
        return new Game(players, maxDouble, handCapacity, rules, dealer, orderers, swiftTurns, evaluators, stopConditions, winConditions, couples);
    }

    /// <summary>
    /// Verifica que sea posible un juego con los valores de los parámetros dados.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que representa si se puede o no efectuar el juego.
    /// </returns>
    /// <param name="maxDouble"> máximo doble de las fichas.</param>
    /// <param name="handCapacity"> cuantas fichas tiene cada mano.</param>
    /// <param name="players"> cuantos jugadores participan.</param>
    private static bool DoublesAndMax(int maxDouble, int handCapacity, int players)
    {
        int boxCapacity = 0;

        // la cantidad de fichas que hay en la caja es la sumatoria de cada doble más 1
        // es decir: (0+1) + (1+1) + (2+1)...
        for (int i = 0; i <= maxDouble; i++)
        {
            boxCapacity += i + 1;
        }

        // si hay menos fichas que las que le corresponden a cada jugador devuelve falso.
        if (boxCapacity < handCapacity * players)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Crea la colección de parejas que van a participar en el juego.
    /// </summary>
    /// <returns>
    /// Una colección de <typeparamref name="Couple"/> con las parejas creadas.
    /// </returns>
    /// <param name="players"> jugadores que van a participar en el juego. </param>
    private static IEnumerable<Couple> CreateCouples(IEnumerable<Player> players)
    {
        // colección que se va a retornar.
        IList<Couple> forReturn = new List<Couple>();

        // crea una nueva lista con los mismos jugadores para que no modifique el parametro players.
        IList<Player> playersClone = new List<Player>(players);
        
        // cantidad de parejas que tendrá el juego.
        int matches = players.Count() / 2;

        // el usuario selecciona las parejas.
        for (int i = 0; i < matches; i++)
        {
            Console.Clear();
            Player PL1 = Auxiliar<Player>.Selector("Seleccione un Jugador", playersClone);
            playersClone.Remove(PL1);

            Player PL2 = Auxiliar<Player>.Selector("Seleccione a su pareja", playersClone);
            playersClone.Remove(PL2);

            // devuelve las parejas.
            forReturn.Add(new Couple(PL1, PL2));
        }

        return forReturn;
    }
}