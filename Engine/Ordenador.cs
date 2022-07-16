namespace Engine
{
    // encapsula la responsabilidad de ordenar la lista de jugadores
    public interface IOrdenador
    {
        // ordena bajo cierto criterio
        void Ordena(List<Player> players, List<Pareja> parejas);
    }

    // ordena los jugadores aleatoriamente
    public class RandomOrd : IOrdenador
    {
        public void Ordena(List<Player> players, List<Pareja> parejas)
        {
            int pos = 0;
            List<Player> aux = new List<Player>();
            Random r = new Random();

            while (players.Count != 0)
            {
                pos = r.Next(players.Count);
                aux.Add(players[pos]);
                players.RemoveAt(pos);

            }

            for (int i = 0; i < aux.Count; i++)
            {
                players.Add(aux[i]);
            }

        }

        public override string ToString()
        {
            return "Ordenador de turnos aleatorios";
        }
    }

    // ordenador clasico por parejas
    // jugador1,adversario1,jugador2,adversario2
    public class ParejasOrd : IOrdenador
    {
        public void Ordena(List<Player> players, List<Pareja> parejas)
        {
            // in case the user ads the couple manager and selects no couples
            if(parejas.Count == 0)
            {
                return;
            }

            players.Clear();
            for (int i = 0; i < parejas.Count; i++)
            {
                players.Add(parejas[i].Player1);

            }
            
            for (int i = 0; i < parejas.Count; i++)
            {
                players.Add(parejas[i].Player2);
            }

        }

        public override string ToString()
        {
            return "Ordenador de turnos por pareja";
        }
    }

    // ordena los jugadores de menor a mayor
    // por la cantidad de puntos de sus manos
    public class ValueOrd : IOrdenador
    {
        public void Ordena(List<Player> players, List<Pareja> parejas)
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players.Count - 1; j++)
                {
                    if (players[j].Puntos < players[j + 1].Puntos)
                    {
                        Swap(players, j, j + 1);
                    }
                }
            }
        }

        private void Swap(List<Player> PL, int a, int b)
        {
            Player temp = PL[a];
            PL[a] = PL[b];
            PL[b] = temp;
        }

        public override string ToString()
        {
            return "Ordenador de turnos por valor";
        }
    }
}