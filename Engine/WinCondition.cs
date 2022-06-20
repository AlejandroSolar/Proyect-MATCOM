namespace Engine
{

    public interface IWinCondition
    {
        public List<Player> Ganadores(List<Player> jugadores);
    }

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
    }

    public abstract class PointcountWin : IWinCondition
    {
        protected int puntos {get; set;}

        public virtual List<Player> Ganadores(List<Player> jugadores)
        {
            List<Player> final = new List<Player>();
            
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i].Puntos() == puntos)
                {
                    final.Add(jugadores[i]);
                }
            }

            return final;
        }
    }

    public class PointcountWinLower : PointcountWin
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
                int aux = jugadores[i].Puntos();
                if (puntos > aux)
                {
                    puntos = aux;
                }
            }
            return puntos;
        }
    }


    public class PointcountWinHigher : PointcountWin
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
                int aux = jugadores[i].Puntos();
                if (puntos <= aux)
                {
                    puntos = aux;
                }
            }
            return puntos;
        }
    }
}