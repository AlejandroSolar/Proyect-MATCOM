namespace Engine
{
    // Condiciones para otorgar la victoria en el juego
    public interface IWinCondition
    {
        // retorna la lista de jugadores que resultaron ganadores
        List<Player> Ganadores(List<Player> jugadores);
    }

    // si alguno de los jugadores se pegó automaticamente le otorga la victoria
    public class PegueWin : IWinCondition
    {
        public List<Player> Ganadores(List<Player> jugadores)
        {
            List<Player> final = new List<Player>();

            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].Mano?.Count == 0)
                {
                    final.Add(jugadores[i]);
                }
            }

            return final;
        }

        public override string ToString()
        {
            return "Victoria por pegue";
        }
    }

    // otorga la victoria de acuerdo a la cantidad de puntos que tenga en la mano cada jugador
    public abstract class PointWin : IWinCondition
    {
        protected int puntos {get; set;}

        public virtual List<Player> Ganadores(List<Player> jugadores)
        {
            List<Player> final = new List<Player>();
            
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].Puntos == puntos)
                {
                    final.Add(jugadores[i]);
                }
            }

            return final;
        }

        public abstract override string ToString();
    }

    // otorga la victoria al jugador que menos puntos tenga en la mano
    public class LowerWin : PointWin
    {
        public override List<Player> Ganadores(List<Player> jugadores)
        {
            ContarPuntos(jugadores);
            return base.Ganadores(jugadores);
        }

        private int ContarPuntos(List<Player> jugadores)
        {
            this.puntos = int.MaxValue;

            for (int i = 0; i < jugadores.Count; i++)
            {
                int aux = jugadores[i].Puntos;
                if (puntos > aux)
                {
                    puntos = aux;
                }
            }
            return puntos;
        }

        public override string ToString()
        {
            return "Victoria por Menor cantidad de puntos";
        }
    }


    // otorga la victoria al jugador que mayor cantidad de puntos tenga en la mano
    public class HigherWin : PointWin
    {
        public override List<Player> Ganadores(List<Player> jugadores)
        {
            ContarPuntos(jugadores);
            return base.Ganadores(jugadores);
        }

        private int ContarPuntos(List<Player> jugadores)
        {
            this.puntos = 0;

            for (int i = 0; i < jugadores.Count; i++)
            {
                int aux = jugadores[i].Puntos;
                if (puntos <= aux)
                {
                    puntos = aux;
                }
            }
            return puntos;
        }

        public override string ToString()
        {
            return "Victoria por Mayor cantidad de puntos";
        }
    }
}