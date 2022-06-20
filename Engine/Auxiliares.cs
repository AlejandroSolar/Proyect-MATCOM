namespace Engine;

public static class Auxiliar
{
    public static bool Validpos(int X, List<Ficha> J)
    {
        {
            if (X < 0 || X >= J.Count)
            {
                return false;
            }
            return true;
        }
    }
}