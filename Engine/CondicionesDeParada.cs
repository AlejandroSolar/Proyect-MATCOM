namespace Engine
{
    // Condiciones de Parada
    public interface IStopCondition
    {
        // alerta de cuando se cumple una condición de parada del juego
        bool Stop(List<Player> players, Board mesa, List<IValid> rules);
    }

    // Cuando uno de los jugadores se quedo sin fichas en la mano
    // el clásico pegue
    public class PegueStop : IStopCondition
    {
        public bool Stop(List<Player> players, Board mesa, List<IValid> rules)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Mano?.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return "Parada por pegue de un Jugador";
        }
    }

    // ningún jugador es capaz de jugar de acuerdo a ninguna regla
    // el clásico tranque
    public class TranqueStop : IStopCondition
    {

        public bool Stop(List<Player> players, Board mesa, List<IValid> reglas)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (!Ronda.Pasate(players[i], reglas, mesa))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "Parada por tranque del juego";
        }
    }

    // detiene el juego si en algún momento todos los jugadores tienen la misma cantidad de puntos en su mano
    public class EqualsStop : IStopCondition
    {
        public bool Stop(List<Player> players, Board mesa, List<IValid> reglas)
        {
            for (int i = 0; i < players.Count - 1; i++)
            {
                if (players[i].Puntos != players[i + 1].Puntos)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return "Parada por igualdad de puntos";
        }
    }

    
}
