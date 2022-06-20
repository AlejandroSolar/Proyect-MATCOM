namespace Engine;

public interface IContador
{
    public int Contador (int L, int R);
}

public class DefaultCount : IContador
{
    public int Contador (int L, int R)
    {
        return L+R;
    }
}

public class DoubleCount : IContador
{
    public int Contador (int L, int R)
    {
        if( L == R )
        {
            return (L+R);
        }
        else return 0;
    }
}