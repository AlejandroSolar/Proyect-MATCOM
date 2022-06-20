namespace Engine
{
    public class Juego
    {
        public Juego(List<Player> players, int FichasBox, int FichasMano, List<IValid> reglas, IRepartidor repa, List<IOrdenador> ordenadores,
        List<IContador> contadores, bool magic, List<IStopCondition> Sconditions, List<IWinCondition> Wconditions, List<Pareja>? parejas = null)
        {
            this.Players = players;
            this.Parejas = parejas;
            this.WConditions = Wconditions;
            this.SConditions = Sconditions;
            this.Mesa = new List<Ficha>();
            this.Box = new List<Ficha>();
            this.Reglas = reglas;
            FillBox(FichasBox, contadores);
            repa.Repartir(Players, Box, FichasMano);
            ordenadores[0].Ordena(Players, Parejas);
            if (magic)
            {
                Magic = magic;
                Console.WriteLine("Cuantas fichas mágicas por jugador");
                int magicas = int.Parse(Console.ReadLine());
                reglas.Add(new MagicValid());
                MagicRepa asd = new MagicRepa();
                asd.Repartir(players, Box, magicas);
            }
        }

        public bool Magic;
        public List<IStopCondition> SConditions { get; set; }

        public List<IWinCondition> WConditions { get; set; }

        public List<IOrdenador> Ordenadores { get; set; }

        public List<IValid> Reglas { get; set; }

        public List<Player> Players { get; set; }

        public List<Player> Ganadores { get; set; }

        public List<Pareja> Parejas { get; set; }

        public List<Ficha> Mesa { get; set; }

        public List<Ficha> Box { get; set; }



        public void FillBox(int max, List<IContador> contadores)
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

        public void RunGame()
        {
            while (true)
            {
                if (Ronda.Run(Players, Reglas, SConditions, Mesa, Magic))
                {
                    break;
                }
            }

            for (int i = 0; i < WConditions.Count; i++)
            {
                this.Ganadores = WConditions[i].Ganadores(Players);

                if (this.Ganadores != null)
                {
                    break;
                }
            }

            if(Parejas != null)
            {
                PairWin();
            }
            
            GameOver();

        }

        public void GameOver()
        {
            Console.Clear();
            if (Ganadores.Count == 0)
            {
                Console.WriteLine("Nadie ganó");
            }
            
            else
            {
                Console.WriteLine();
                Console.WriteLine("Ganador(es):");

                for (int i = 0; i < Ganadores.Count; i++)
                {
                    Console.WriteLine(Ganadores[i].Nombre);
                }

                Players = Players.OrderBy(X => X.Puntos()).ToList();


                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < Players.Count; i++)
                {
                    Console.WriteLine($"{Players[i].Nombre}: {Players[i].Puntos()} Puntos ");
                    Players[i].PrintHand();
                    Console.WriteLine();
                    Console.WriteLine();


                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Estado Final de la Mesa:");
                Ronda.printMesa(Mesa);

            }
        }

        public void PairWin()
        {
            int counter = Ganadores.Count;
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
    }
}