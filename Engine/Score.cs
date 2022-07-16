namespace Engine
{
    // forma de premiar al ganador del torneo
    public interface IScoring
    {
        // actualiza la tabla de posiciones del torneo
        void Premio(List<Player> Players, List<Player> Ganadores, Dictionary<string, int> Tabla);
    }

    // otorga puntos por ganar un juego
    public class WinP : IScoring
    {
        public void Premio(List<Player> Players, List<Player> Ganadores, Dictionary<string, int> Tabla)
        {
            for (int i = 0; i < Ganadores.Count; i++)
            {
                Tabla[Ganadores[i].Nombre] += 1;
            }
        }

        public override string ToString()
        {
            return "Puntos por Victoria";
        }
    }

    // otorga puntos de acuedo a las puntuaciones de las manos de los perdedores
    public class PointP : IScoring
    {
        public void Premio(List<Player> Players, List<Player> Ganadores, Dictionary<string, int> Tabla)
        {
            int cantidad = 0;

            for (int i = 0; i < Players.Count; i++)
            {
                if (!Ganadores.Contains(Players[i]))
                {
                    cantidad += Players[i].Puntos;
                }
            }

            for (int i = 0; i < Ganadores.Count; i++)
            {
                Tabla[Ganadores[i].Nombre] += cantidad / Ganadores.Count;
            }
        }

        public override string ToString()
        {
            return "Puntos por puntuación de los perdedores";
        }
    }
}