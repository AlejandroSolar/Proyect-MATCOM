namespace Engine;

public interface IOrdenador
{
    public void Ordena(List<Player> players, List<Pareja> parejas);
}

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
}

public class ParejasOrd : IOrdenador
{
    public void Ordena(List<Player> players, List<Pareja> parejas)
    {
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
}