namespace Engine
{
    public abstract class Player
    {

        public abstract List<Ficha> Mano { get; set; }

        public abstract string Nombre { get; set; } 

        public abstract int AskPosition();

        public abstract Ficha Play(List<Ficha> mesa, List<IValid> reglas);

        public void PrintHand()
        {
            for (int i = 0; i < Mano?.Count; i++)
            {
                Console.Write($"{Mano[i]}  ");
            }
        }

        public int Puntos()
        {
            int Count = 0;

            for (int i = 0; i < Mano?.Count; i++)
            {
                Count += Mano[i].Valor;
            }

            return Count;
        }

    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string nombre)
        {
            this.Nombre = nombre;
            this.Mano = new List<Ficha>();
        }

        public override List<Ficha> Mano { get; set; }

        public override string Nombre { get; set; }

        public override Ficha Play(List<Ficha> mesa, List<IValid> reglas)
        {
            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                if (!Validpos(input, Mano))
                {
                    Console.WriteLine("Posicion no elegible, vuelva a seleccionar la ficha");
                    continue;
                }


                for (int i = 0; i < reglas.Count; i++)
                {
                    if (reglas[i].Valid(Mano[input], mesa) != -1)
                    {
                        return Mano[input];
                    }
                }


                Console.WriteLine("La jugada no es válida");
                Thread.Sleep(2000);

            }
        }

        public override int AskPosition()
        {
            Console.WriteLine("La Ficha que desea jugar se puede colocar en mas de una posición");
            Console.WriteLine("Elija en cual desea jugarla:");
            Console.WriteLine();
            Console.WriteLine("[I] Izquierda");
            Console.WriteLine("[D] Derecha");

            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.I:
                        return 0;

                    case ConsoleKey.D:
                        return 1;

                    default:
                        break;
                }
            }
        }

        static bool Validpos(int X, List<Ficha> J)
        {
            if (X < 0 || X >= J.Count)
            {
                return false;
            }
            return true;
        }

    }

    public class PCPlayer : Player 
    {
        public override string Nombre { get; set; }

        public override List<Ficha> Mano { get; set; }

        public IStrategy Estrategia {get; set; }

        public PCPlayer(string nombre, IStrategy estrategia)
        {
            this.Nombre = nombre;
            this.Mano =  new List<Ficha>();
            this.Estrategia = estrategia;
        }

        public override Ficha Play(List<Ficha> mesa, List<IValid> reglas)
        {
            List<Ficha> posibles = new List<Ficha>();

            for (int i = 0; i < Mano.Count; i++)
            {
                for (int j = 0; j < reglas.Count; j++)
                {
                    if (reglas[j].Valid(Mano[i],mesa) != -1)
                    {
                        posibles.Add(Mano[i]);
                        break;
                    }
                }
            }

            return Estrategia.Jugada(posibles);
        }

        public override int AskPosition()
        {
            return Estrategia.Askpos();
        }
    }

}