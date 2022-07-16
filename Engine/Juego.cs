namespace Engine
{
    // entidad que representa al juego
    public class Juego
    {
        public Juego(

            List<Player> players,
            int FichasBox,
            int FichasMano,
            List<IValid> reglas,
            IRepartidor repartidor,
            List<IOrdenador> ordenadores,
            int swiftTurns,
            List<IEvaluador> Evaluadores,
            List<IStopCondition> Sconditions,
            List<IWinCondition> Wconditions,
            List<Pareja> parejas

        )

        {
            this.Players = players;
            this.Parejas = parejas;
            this.WConditions = Wconditions;
            this.SConditions = Sconditions;
            this.Mesa = new Board();
            this.Box = new List<Ficha>();
            this.Reglas = reglas;
            this.Ordenadores = ordenadores;
            this.SwiftTurns = swiftTurns;
            this.Registro = new Register();

            // crea las fichas y las almacena en una caja para ser posteriormente repartidas
            FillBox(FichasBox, Evaluadores);

            // reparte las fichas a cada jugador
            repartidor.Repartir(players, Box, FichasMano);

            // plantea el orden en el que van a jugar los jugadores
            ordenadores[0].Ordena(Players, Parejas);
        }

        public Register Registro { get; private set; }

        // lista de condiciones de parada
        private List<IStopCondition> SConditions { get; }

        // lista de condiciones de victoria
        private List<IWinCondition> WConditions { get; }

        // lista de ordenadores de los turnos de los jugadores
        private List<IOrdenador> Ordenadores { get; }

        // lista de reglas
        private List<IValid> Reglas { get; }

        // lista de jugadores
        private List<Player> Players { get; set; }

        // ganadores del juego
        public List<Player>? Ganadores { get; private set; }

        // lista de parejas si las hay
        private List<Pareja> Parejas { get; }

        // mesa del juego
        private Board Mesa { get; set; }

        // almacen de fichas 
        private List<Ficha> Box { get; set; }

        // número de turnos antes de cambiar de ordenador
        private int SwiftTurns { get; }


        // crear las fichas y almacenarlas de acuerdo al doble máximo elegido por el jugador
        private void FillBox(int max, List<IEvaluador> contadores)
        {
            for (int i = 0; i <= max; i++)
            {
                for (int j = i; j <= max; j++)
                {
                    int supp = 0;

                    for (int k = 0; k < contadores.Count; k++)
                    {
                        supp += contadores[k].Contador(i, j);
                    }

                    Box.Add(new Ficha(i, j, supp));
                }
            }
        }

        // Juega el juego
        public void RunGame()
        {
            // cantidad de rondas que han pasado hasta el momento
            int Rondas = 0;

            // ordenador actual
            int CurrentOrd = 1;

            while (true)
            {
                Rondas += 1;

                // si alguna de las condiciones de parada detiene la ronda automaticamente detiene el juego
                if (Ronda.Run(Players, Reglas, SConditions, Mesa, Registro,Parejas))
                {
                    break;
                }

                // si el número de rondas alcanza el escogido por el usuario, cambia de ordenador 
                // y vuelve a ordemar los turnos de los jugadores
                if (Rondas == SwiftTurns && SwiftTurns != 0)
                {
                    Swift(ref CurrentOrd);
                    Rondas = 0;
                }
            }

            // aplica las condiciones de victoria cuando el juego se detiene
            for (int i = 0; i < WConditions.Count; i++)
            {
                this.Ganadores = WConditions[i].Ganadores(new List<Player>(Players)/*clon*/);

                if (this.Ganadores.Count > 0)
                {
                    break;
                }
            }

            // si hay división en parejas otorga la victoria a la pareja
            if (Parejas.Count > 0)
            {
                PairWin();
            }

            // finaliza el juego
            GameOver();
        }

        // imprime el estado del juego y el registro
        private void GameOver()
        {
            Console.Clear();
            if (Ganadores!.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hubo ganadores en esta partida");
                Console.ResetColor();
            }

            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ganador(es):");
                Console.ForegroundColor = ConsoleColor.Green;

                for (int i = 0; i < Ganadores.Count; i++)
                {
                    Console.WriteLine(Ganadores[i].Nombre);
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------");
                Console.ResetColor();
            }
            Players = Players.OrderBy(X => X.Puntos).ToList();

            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < Players.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{Players[i].ToString()}");
                Console.ResetColor();
                Console.WriteLine($": {Players[i].Puntos} Puntos ");
                Players[i].PrintHand();
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Estado Final de la Mesa:");
            Console.ResetColor();
            System.Console.WriteLine(Mesa);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Pulse Enter");
            Console.ReadLine();

            Registro.PrintRegister();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fin de la partida.\n");
            Console.ResetColor();

            Console.WriteLine("Pulse Enter");
            Console.ReadLine();
            
        }

        // si hay división en parejas otorga la victoria a la pareja
        private void PairWin()
        {
            int counter = Ganadores!.Count;
            for (int j = 0; j < counter; j++)
            {
                for (int i = 0; i < Parejas.Count; i++)
                {
                    if (Parejas[i].Player1.Equals(Ganadores[j]) && !Ganadores.Contains(Parejas[i].Player2))
                    {
                        Ganadores.Add(Parejas[i].Player2);
                    }
                    else if (Parejas[i].Player2.Equals(Ganadores[j]) && !Ganadores.Contains(Parejas[i].Player1))
                    {
                        Ganadores.Add(Parejas[i].Player1);
                    }
                }
            }
        }

        // cambia de ordenador
        private void Swift(ref int CurrentOrd)
        {
            if (Ordenadores.Count == 1)
            {
                CurrentOrd = 0;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Aplicando {Ordenadores[CurrentOrd].ToString()}...");
            Console.ResetColor();
            Thread.Sleep(2000);
            Ordenadores[CurrentOrd].Ordena(Players, Parejas);
            CurrentOrd += 1;
            if (CurrentOrd == Ordenadores.Count)
            {
                CurrentOrd = 0;
            }
        }
    }
}