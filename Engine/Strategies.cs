namespace Engine;

public interface IStrategy
{
    public Ficha Jugada(List<Ficha> posibles);

    public int Askpos();
}

public abstract class BasicPlay : IStrategy
{
    public abstract Ficha Jugada(List<Ficha> posibles);

    public int Askpos()
    {
        Random r = new Random();
        return r.Next(0, 2);
    }
}

public class BotagordaST : BasicPlay
{
    public override Ficha Jugada(List<Ficha> posibles)
    {
        int bestSum = 0;
        int index = 0;
        for (int i = 0; i < posibles.Count; i++)
        {
            int ActualSum = posibles[i].PartIzq + posibles[i].PartDch;
            if (bestSum < ActualSum)
            {
                bestSum = ActualSum;
                index = i;
            }
        }

        return posibles[index];
    }
}

public class RandomST : BasicPlay
{
    public override Ficha Jugada(List<Ficha> posibles)
    {
        Random r = new Random();
        return posibles[r.Next(posibles.Count)];

    }

}

public class BotaDobleST : BasicPlay
{
    public override Ficha Jugada(List<Ficha> posibles)
    {
        for (int i = 0; i < posibles.Count; i++)
        {
            if (posibles[i].PartIzq == posibles[i].PartDch)
            {
                return posibles[i];
            }
        }
        return posibles[0];
    }
}

