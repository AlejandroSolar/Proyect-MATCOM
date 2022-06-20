namespace Engine;

public interface IWinTournament
{
    public bool Win(Dictionary<string,int> registro,int n);

}

public class PuntosWin : IWinTournament
{
    public bool Win(Dictionary<string,int> registro,int puntos)
    {
        for (int i = 0; i < registro.Count; i++)
        {
            if(registro.ElementAt(i).Value >= puntos)
            {
                return true;
            }
        }
        return false;
    }
}

