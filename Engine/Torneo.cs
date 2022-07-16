namespace Engine
{
    // entidad encargada de que se pueda jugar en modalidad torneo
    public class Tournament
    {

        public Tournament(int puntos, int partidas, IScoring premio, List<Player> players)
        {
            this.Puntos = puntos;
            this.Players = players;
            this.Partidas = partidas;
            this.Premio = premio;
            this.TablaPosiciones = new Dictionary<string, int>();

            // llena la table de posiciones
            for (int i = 0; i < players.Count; i++)
            {
                TablaPosiciones.Add(players[i].Nombre, 0);
            }
        }
        // puntos que se deben alcanzar para ganar el torneo
        private int Puntos { get; }

        // jugadores que van a participar
        private int Partidas { get; }

        // número de partidas que se van a jugar
        private IScoring Premio { get; }

        // como se les otorga puntos a los vencedores
        public Dictionary<string, int> TablaPosiciones { get; private set; }

        // tabla de posiciones de los jugadores (nombre, puntuación actual)
        private List<Player> Players { get; }

        // empieza el torneo
        public void Run()
        {
            for (int i = 0; i < Partidas; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Partida #{i + 1}");
                Console.ResetColor();
                Thread.Sleep(2000);

                // crea el juego acorde a las elecciones del usuario
                Juego X = GameCreator.Game(Players);
                
                // comienza el juego
                X.RunGame();

                // premia a los ganadores
                Premio.Premio(Auxiliar.ClonePlayers(Players), Auxiliar.ClonePlayers(X.Ganadores!), TablaPosiciones);

                // imprime la tabla de posiciones
                PrintTabla();

                // si se cumplieron los requisitos de victoria del torneo, termina el proceso
                if (Win(TablaPosiciones, Puntos))
                {
                    break;
                }
            }
            // finaliza el torneo
            GameOver();
        }

        // si se cumplió el requisito de finalización del torneo
        private bool Win(Dictionary<string, int> tabla, int puntos)
        {
            for (int i = 0; i < tabla.Count; i++)
            {
                if (tabla.ElementAt(i).Value >= puntos)
                {
                    return true;
                }
            }
            return false;
        }

        // muestra la tabla de posiciones
        // y crea e imprime el listado con el/los ganador/es
        private void GameOver()
        {
            var FinalScoreBoard = TablaPosiciones.OrderByDescending(X => X.Value);
            List<string> Winners = new List<string>();

            int Count = 0;
            int MaxScore = 0;

            foreach (var item in FinalScoreBoard)
            {
                if (Count == 0)
                {
                    Winners.Add(item.Key);
                    MaxScore = item.Value;
                    Count++;
                    continue;
                }

                if (item.Value != MaxScore)
                {
                    break;
                }

                Winners.Add(item.Key);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("TORNEO TERMINADO");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Ganadores:");
            Console.WriteLine("----------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();

            foreach (var item in Winners)
            {
                Console.WriteLine(item);
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enter para ver la tabla de posiciones");
            Console.ReadLine();

            PrintTabla();
        }

        // metodo encargado de imprimir la tabla de posiciones
        private void PrintTabla()
        {
            var B = TablaPosiciones.OrderByDescending(X => X.Value);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tabla de Posiciones:");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            Console.ResetColor();

            foreach (var item in B)
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
}