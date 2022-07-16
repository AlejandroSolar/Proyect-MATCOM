namespace Engine
{
    public class Player
    {
        public Player(string nombre, IStrategy estrategia)
        {
            this.Nombre = nombre;
            this.Mano = new List<Ficha>();
            this.Estrategia = estrategia;
        }

        // mano del jugador
        public List<Ficha> Mano { get; private set;}

        public int Puntos { get { return puntos(); } }

        // nombre del jugador
        public string Nombre { get; }

        public IStrategy Estrategia { get; }

        // retorna la jugada que se va a hacer
        public Jugada Play(Board mesa, List<IValid> reglas, Register registro, List<Pareja> parejas)
        {
            List<Jugada> posibles = Auxiliar.LlenarPosibles(Mano, reglas, mesa);
            return this.Estrategia.Jugada(new List<Ficha>(this.Mano), posibles, mesa, registro, new List<Pareja>(parejas));
        }

        // imprime la mano del jugador
        public void PrintHand()
        {
            for (int i = 0; i < Mano?.Count; i++)
            {
                Console.Write($"{Mano[i]}  ");
            }
        }

        // puntos del jugador, es la suma de los puntos de cada ficha en la mano
        private int puntos()
        {
            int Count = 0;

            for (int i = 0; i < Mano?.Count; i++)
            {
                Count += Mano[i].Valor;
            }

            return Count;
        }

        // representación string del jugador
        public override string ToString()
        {
            return $"{this.Nombre} ({this.Estrategia.ToString()})";
        }

        //clona el jugador
        public Player Clone()
        {
            Player x = new Player(this.Nombre,this.Estrategia);
            x.Mano = Auxiliar.CloneList(this.Mano);
            return x;

        }

    }
}