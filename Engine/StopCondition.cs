namespace Engine
{

    public interface IStopCondition 
    {
        bool Stop(List<Player> players, List<Ficha> Mesa, List<IValid> reglas);
    }

    public class Pegue : IStopCondition
    {
        public bool Stop(List<Player> players, List<Ficha> Mesa, List<IValid> reglas)
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
    }

    public class tranque : IStopCondition
    {

        public bool Stop(List<Player> players,List<Ficha> Mesa, List<IValid> reglas)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (!Ronda.Pasate(players[i],reglas,Mesa))
                {
                    return false;
                }
            }
            return true;
        }
    }

}
