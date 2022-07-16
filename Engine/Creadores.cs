namespace Engine
{   
    // entidad encargada de crear la lista de jugadores que van a participar en el juego
    public static class PlayerCreator
    {
        // creo una lista con todas las implementaciones actuales de estrategia
        private static List<IStrategy> Estrategias = Reflection<IStrategy>.ListCreator(Reflection<IStrategy>.TypesListCreator());
        
        // crea la lista de jugadores
        public static List<Player> Players()
        {
            // la lista actual
            List<Player> players = new List<Player>();

            // el usuario elige cuantos jugadores van a participar
            int n = Auxiliar.NumberSelector("Elija la cantidad de jugadores", 1);

            // asigna a cada jugador un nombre y una estrategia a elección del usuario
            for (int i = 0; i < n; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Diga el nombre del jugador {0}", i + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string nombre = Console.ReadLine()!;
                if (string.IsNullOrEmpty(nombre))
                {
                    nombre = $"Player {i + 1}";
                }

                bool Repited = false;
                for (int J = 0; J < players.Count; J++)
                {
                    if (nombre == players[J].Nombre)
                    {
                        Repited = true;
                        break;
                    }
                }
                // no permitir que el usuario le ponga el mismo nombre a varios jugadores para evitar confusiones
                if (Repited)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre ya existente!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    i -= 1;
                    continue;
                }

                // el usuario elige la estrategia
                IStrategy strategy = Auxiliar<IStrategy>.Selector($"Elige la estrategia que usara {nombre}", Estrategias);
                
                Type x = strategy.GetType();
                // añade a la lista el jugador con su nombre y estrategias correspondientes
                players.Add(new Player(nombre, Reflection<IStrategy>.DefaultInstanceCreator(x)));
            }
            return players;
        }
    }

    // entidad encargada de crear el juego
    public static class GameCreator
    {
        // contiene instancias de todas las implementaciones que se tienen de cada aspecto del juego

        // las formas de dar valor a las fichas
        private static List<Type> Evaluadores = Reflection<IEvaluador>.TypesListCreator();
        
        // las formas de validar la jugada (reglas)  
        private static List<Type> Validadores = Reflection<IValid>.TypesListCreator();

        // las formas de ordenar la lista de jugadores
        private static List<Type> Ordenadores = Reflection<IOrdenador>.TypesListCreator();
        
        // las formas de repartirles las fichas a los jugadores
        private static List<Type> Repartidores = Reflection<IRepartidor>.TypesListCreator();

        // las condiciones de parada del juego
        private static List<Type> Stops = Reflection<IStopCondition>.TypesListCreator();
        
        // las condiciones de victoria del juego
        private static List<Type> WinConditions = Reflection<IWinCondition>.TypesListCreator();

        // configuraciones predeterminadas
        private static List<Type> DefaultOptions = Reflection<IDefaultGames>.TypesListCreator();

       
        // Método encargado de crear el juego
        public static Juego Game(List<Player> Jugadores)
        {
            // máximo doble que se va
            int doble = 0;

            // cantidad de fichas que va a tener cada mano
            int max = 0;

            // cantidad de turnos que han de pasar para cambiar el orden los turnos de los jugadores
            int SwiftTurns = 0;

            IRepartidor Repartidor = null!;

            List<IEvaluador> evaluadores = new List<IEvaluador>();
            List<IValid> validadores = new List<IValid>();
            List<IOrdenador> ordenadores = new List<IOrdenador>();
            List<IStopCondition> stops = new List<IStopCondition>();
            List<IWinCondition> winConditions = new List<IWinCondition>();
            List<Pareja> parejas = new List<Pareja>();
            List<IDefaultGames> defaultOptions = Reflection<IDefaultGames>.ListCreator(DefaultOptions);

            // pregunta si se va a jugar por parejas y si es así las crea
            if ((Jugadores.Count % 2 == 0) && (Auxiliar.YesOrNo("¿Desea jugar por parejas?")))
            {
                parejas = LlenarParejas(Jugadores);
            }

            ConfigFilter(Jugadores.Count, defaultOptions);

            if (defaultOptions.Count > 0 && Auxiliar.YesOrNo("¿Desea utilizar alguna configuración por defecto?"))
            {
                IDefaultGames Default = Auxiliar<IDefaultGames>.Selector("Seleccione un juego predeterminado.", defaultOptions);

                doble = Default.Doble;
                max = Default.Max;
                SwiftTurns = Default.SwiftTurns;
                Repartidor = Default.Repartidor;
                evaluadores = Default.evaluadores;
                validadores = Default.validadores;
                stops = Default.stops;
                winConditions = Default.winConditions;
                ordenadores = Default.ordenadores;
            }

            else
            {
                while (true)
                {
                    doble = Auxiliar.NumberSelector("Elija el doble máximo en las fichas", 0);
                    max = Auxiliar.NumberSelector("Elija la cantidad de fichas en la mano de cada jugador", 1);

                    if (!DoblesandMax(doble, max, Jugadores.Count))
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

                Auxiliar<IStopCondition>.ConfigSelector(stops, Stops, "Elija una condición de parada para la partida");
                Auxiliar<IWinCondition>.ConfigSelector(winConditions, WinConditions, "Elija una condición de Victoria para la partida");
                Auxiliar<IValid>.ConfigSelector(validadores, Validadores, "Elija un Validador de jugadas para la partida");
                Auxiliar<IEvaluador>.ConfigSelector(evaluadores, Evaluadores, "Elija un evaluador de fichas para la partida");
                Auxiliar<IOrdenador>.ConfigSelector(ordenadores, Ordenadores, "Elija un ordenador de turnos para la partida");

                if (ordenadores.Count > 1)
                {
                    SwiftTurns = Auxiliar.NumberSelector("Ha elegido mas de un ordenaror de turnos \nElija cada cuantas rondas estos cambiaran el orden de los jugadores", 1);
                }

                else if (ordenadores.Count == 1 && Auxiliar.YesOrNo("¿Desea reordenar el juego tras una cierta cantidad de turnos?"))
                {
                    SwiftTurns = Auxiliar.NumberSelector("¿Cada Cuantos turnos se reordenaran los jugadores?", 1);
                }

                Repartidor = Auxiliar<IRepartidor>.Selector("Elija el repartidor de fichas de la partida", Reflection<IRepartidor>.ListCreator(Repartidores));
            }

            return new Juego(Jugadores, doble, max, validadores, Repartidor, ordenadores, SwiftTurns, evaluadores, stops, winConditions, parejas);
        }

        // verifica que la cantidad de fichas en la mano de cada jugador este acorde a la cantidad
        // total de fichas que hay en el juego
        private static bool DoblesandMax(int doble, int max, int players)
        {
            int cantfichas = 0;
            for (int i = 0; i <= doble; i++)
            {
                cantfichas += i + 1;
            }

            if (cantfichas < max * players)
            {
                return false;
            }

            return true;
        }

        // elimina configuraciones no permitidas
        private static void ConfigFilter(int players, List<IDefaultGames> defaultOptions)
        {
            for (int i = defaultOptions.Count - 1; i >= 0; i--)
            {
                if (!DoblesandMax(defaultOptions[i].Doble, defaultOptions[i].Max, players))
                {
                    defaultOptions.RemoveAt(i);
                }
            }
        }

        // se encarga de formar las parejas de jugadores
        private static List<Pareja> LlenarParejas(List<Player> Jugadores)
        {
            List<Pareja> Final = new List<Pareja>();
            List<Player> players = new List<Player>(Jugadores);
            int Matches = Jugadores.Count / 2;

            for (int i = 0; i < Matches; i++)
            {
                Console.Clear();
                Player PL1 = Auxiliar<Player>.Selector("Seleccione un Jugador", players);
                players.Remove(PL1);

                Player PL2 = Auxiliar<Player>.Selector("Seleccione a su pareja", players);
                players.Remove(PL2);

                Final.Add(new Pareja(PL1, PL2));
            }

            return Final;
        }
    }
    // entidad encargada de la creación de torneos
    public static class TournamentCreator
    {
        private static List<Type> Scoring = Reflection<IScoring>.TypesListCreator();

        public static Tournament Torneo(List<Player> Jugadores)
        {
            int partidas = int.MaxValue;
            int PointLimit = int.MaxValue;
            IScoring scoring;

            if (Auxiliar.YesOrNo("¿Desea jugar con un Límite de Partidas?"))
            {
                partidas = Auxiliar.NumberSelector("¿Cuantas partidas se jugaran en el torneo?", 1);
            }

            if (Auxiliar.YesOrNo("¿Desea jugar con un Límite de Puntos?"))
            {
                PointLimit = Auxiliar.NumberSelector("¿Cuanto se debe puntuar para ganar en el torneo?", 1);
            }
            scoring = Auxiliar<IScoring>.Selector("¿Como se puntuara en el Torneo?", Reflection<IScoring>.ListCreator(Scoring));

            return new Tournament(PointLimit, partidas, scoring, Jugadores);
        }
    }
}