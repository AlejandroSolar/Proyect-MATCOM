namespace Engine;

public interface Ipremio
{
    public void Premio(List<Player> Players, List<Player> Ganadores,Dictionary<string,int> registro);
}

public class WinP : Ipremio
{
    public void Premio(List<Player> Players, List<Player> Ganadores,Dictionary<string,int> registro)
    {
        for (int i = 0; i < Ganadores.Count; i++)
        {
            registro[Ganadores[i].Nombre] += 1; 
        }
    }
} 

public class PointP : Ipremio
{
    public void Premio(List<Player> Players, List<Player> Ganadores,Dictionary<string,int> registro)
    {
        int cantidad = 0;
        
        for (int i = 0; i < Players.Count; i++)
        {
            if(!Ganadores.Contains(Players[i]))
            {
                cantidad += Players[i].Puntos();
            }
        }

        for (int i = 0; i < Ganadores.Count; i++)
        {
            registro[Ganadores[i].Nombre] += cantidad/ Ganadores.Count;   
        }
    }
}